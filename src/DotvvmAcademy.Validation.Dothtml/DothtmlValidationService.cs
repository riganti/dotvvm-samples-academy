﻿using DotVVM.Framework.Compilation;
using DotVVM.Framework.Compilation.ControlTree;
using DotVVM.Framework.Compilation.ControlTree.Resolved;
using DotVVM.Framework.Compilation.Parser.Dothtml.Parser;
using DotVVM.Framework.Compilation.Parser.Dothtml.Tokenizer;
using DotvvmAcademy.Meta;
using DotvvmAcademy.Validation.CSharp;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyModel;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Xml;

namespace DotvvmAcademy.Validation.Dothtml
{
    public class DothtmlValidationService : IValidationService
    {
        private readonly IServiceProvider globalProvider;

        public DothtmlValidationService()
        {
            globalProvider = GetServiceProvider();
        }

        public Task<IEnumerable<IValidationDiagnostic>> Validate(
            IEnumerable<IConstraint> constraints,
            IEnumerable<ISourceCode> sources)
        {
            using var scope = globalProvider.CreateScope();
            var id = Guid.NewGuid();

            // prepare services
            var context = scope.ServiceProvider.GetRequiredService<Context>();
            context.SourceCodeStorage = new SourceCodeStorage(sources);
            var reporter = scope.ServiceProvider.GetRequiredService<IValidationReporter>();
            context.Compilation = GetCompilation(reporter, sources, id);
            var platform = Environment.OSVersion.Platform.ToString();
            var assemblies = DependencyContext.Default.GetRuntimeAssemblyNames(platform)
                .Select(l => Assembly.Load(l.Name))
                .ToImmutableArray();
            context.Converter = new MetaConverter(context.Compilation, assemblies);

            // compile tree
            var sourceCode = sources.OfType<DothtmlSourceCode>().Single();
            var tree = GetResolvedTree(
                reporter,
                scope.ServiceProvider.GetRequiredService<DefaultControlTreeResolver>(),
                scope.ServiceProvider.GetRequiredService<ErrorAggregatingVisitor>(),
                sourceCode);
            if (tree == null)
            {
                return Task.FromResult<IEnumerable<IValidationDiagnostic>>(GetValidationDiagnostics(reporter));
            }
            context.Tree = GetXPathTree(scope.ServiceProvider.GetRequiredService<XPathTreeVisitor>(), tree);

            foreach (var constraint in constraints)
            {
                constraint.Validate(scope.ServiceProvider);
            }
            return Task.FromResult<IEnumerable<IValidationDiagnostic>>(GetValidationDiagnostics(reporter));
        }

        private CSharpCompilation GetCompilation(IValidationReporter reporter, IEnumerable<ISourceCode> sources, Guid id)
        {
            var trees = sources.OfType<CSharpSourceCode>()
                .Select(s => CSharpSyntaxTree.ParseText(s.GetContent()));
            var compilation = CSharpCompilation.Create(
                assemblyName: $"ValidatedUserCode.Dothtml.{id}",
                syntaxTrees: trees,
                references: new[]
                {
                    RoslynReference.FromName("mscorlib"),
                    RoslynReference.FromName("netstandard"),
                    RoslynReference.FromName("System.Private.CoreLib"),
                    RoslynReference.FromName("System.Runtime"),
                    RoslynReference.FromName("System.Collections"),
                    RoslynReference.FromName("System.Reflection"),
                    RoslynReference.FromName("System.Linq"),
                    RoslynReference.FromName("System.Linq.Expressions"),
                    RoslynReference.FromName("System.ComponentModel.Annotations"),
                    RoslynReference.FromName("DotVVM.Framework"),
                    RoslynReference.FromName("DotVVM.Core")
                },
                options: new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));
            var diagnostics = compilation.GetDiagnostics();
            foreach (var diagnostic in diagnostics)
            {
                reporter.Report(diagnostic);
            }
            return compilation;
        }

        private IServiceProvider GetServiceProvider()
        {
            var c = new ServiceCollection();
            DotvvmServiceCollectionExtensions.RegisterDotVVMServices(c);
            c.AddScoped<Context>();
            c.AddTransient(p => p.GetRequiredService<Context>().Converter);
            c.AddTransient(p => p.GetRequiredService<Context>().SourceCodeStorage);
            c.AddTransient(p => p.GetRequiredService<Context>().Tree);
            c.AddTransient(p => p.GetRequiredService<Context>().Compilation);
            c.AddTransient<IAttributeExtractor, AttributeExtractor>();
            c.AddTransient<ITypedConstantExtractor, TypedConstantExtractor>();
            c.AddScoped<IValidationReporter, ValidationReporter>();
            c.AddScoped<SourceCodeStorage>();
            c.AddScoped<NodeLocator>();
            c.AddScoped<XPathTreeVisitor>();
            c.AddScoped<ErrorAggregatingVisitor>();
            c.AddScoped<XPathDothtmlNamespaceResolver>();
            c.AddScoped<NameTable>();
            return c.BuildServiceProvider();
        }

        private ImmutableArray<IValidationDiagnostic> GetValidationDiagnostics(IValidationReporter reporter)
        {
            return reporter.GetDiagnostics()
                .Where(d => d.Source == null || d.Source.IsValidated)
                .ToImmutableArray();
        }

        private ResolvedTreeRoot GetResolvedTree(
            IValidationReporter reporter,
            DefaultControlTreeResolver resolver,
            ErrorAggregatingVisitor visitor,
            DothtmlSourceCode sourceCode)
        {
            try
            {
                // TODO: Figure out how to handle master pages.
                // parse syntax
                var tokenizer = new DothtmlTokenizer();
                tokenizer.Tokenize(sourceCode.GetContent() ?? string.Empty);
                foreach(var token in tokenizer.Tokens)
                {
                    if (token.HasError)
                    {
                        reporter.Report(token.Error, sourceCode);
                    }
                }
                var parser = new DothtmlParser();
                var dothtmlRoot = parser.Parse(tokenizer.Tokens);

                // parse semantics
                var root = (ResolvedTreeRoot)resolver.ResolveTree(dothtmlRoot, sourceCode.FileName);
                root.FileName = sourceCode.FileName;
                root.Accept(visitor);
                return root;
            }
            catch(DotvvmCompilationException exception)
            {
                reporter.Report(exception, sourceCode);
                return null;
            }
        }

        private XPathDothtmlRoot GetXPathTree(XPathTreeVisitor visitor, ResolvedTreeRoot validationTree)
        {
            return visitor.Visit(validationTree);
        }

        private class Context
        {
            public Compilation Compilation { get; set; }

            public MetaConverter Converter { get; set; }

            public SourceCodeStorage SourceCodeStorage { get; set; }

            public XPathDothtmlRoot Tree { get; set; }
        }
    }
}
