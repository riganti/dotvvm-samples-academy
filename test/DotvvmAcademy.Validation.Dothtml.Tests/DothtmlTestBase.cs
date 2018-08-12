using DotVVM.Framework.Compilation.Parser.Dothtml.Parser;
using DotVVM.Framework.Compilation.Parser.Dothtml.Tokenizer;
using DotvvmAcademy.Meta;
using DotvvmAcademy.Validation.Dothtml.ValidationTree;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Immutable;
using System.Linq;
using System.Reflection;

namespace DotvvmAcademy.Validation.Dothtml.Tests
{
    public abstract class DothtmlTestBase
    {
        public DothtmlTestBase()
        {
            Provider = BuildServiceProvider();
        }

        public Guid Id { get; } = Guid.NewGuid();

        protected IServiceProvider Provider { get; }

        protected virtual IServiceProvider BuildServiceProvider()
        {
            var container = new ServiceCollection();
            RegisterServices(container);
            return container.BuildServiceProvider();
        }

        protected virtual CSharpCompilation GetCompilation(string source)
        {
            var tree = CSharpSyntaxTree.ParseText(source ?? string.Empty);
            return CSharpCompilation.Create(
                assemblyName: $"Test_{Id}",
                syntaxTrees: new[] { tree },
                references: new[]
                {
                    RoslynReference.FromName("netstandard"),
                    RoslynReference.FromName("System.Private.CoreLib"),
                    RoslynReference.FromName("System.Runtime"),
                    RoslynReference.FromName("System.Collections"),
                    RoslynReference.FromName("System.Reflection"),
                    RoslynReference.FromName("DotVVM.Framework"),
                    RoslynReference.FromName("DotVVM.Core")
                },
                options: new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));
        }

        protected virtual ValidationTreeRoot GetValidationTree(string dothtml, string viewModel = null)
        {
            var tokenizer = new DothtmlTokenizer();
            tokenizer.Tokenize(dothtml);
            var parser = new DothtmlParser();
            var root = parser.Parse(tokenizer.Tokens);
            using (var scope = Provider.CreateScope())
            {
                var resolver = Provider.GetRequiredService<ValidationTreeResolver>();
                var compilationAccessor = Provider.GetRequiredService<ICSharpCompilationAccessor>();
                var assemblyAccessor = Provider.GetRequiredService<IAssemblyAccessor>();
                assemblyAccessor.Assemblies = Assembly.GetEntryAssembly()
                    .GetReferencedAssemblies()
                    .Select(Assembly.Load)
                    .ToImmutableArray();
                compilationAccessor.Compilation = GetCompilation(viewModel);
                return (ValidationTreeRoot)resolver.ResolveTree(root, "test.dothtml");
            }
        }

        protected virtual void RegisterServices(IServiceCollection container)
        {
            container.AddMetaScopeFriendly();
            container.AddScoped<ValidationControlResolver>();
            container.AddScoped<ValidationTreeBuilder>();
            container.AddScoped<ValidationPropertyFactory>();
            container.AddScoped<ValidationControlMetadataFactory>();
            container.AddScoped<ValidationTypeDescriptorFactory>();
            container.AddScoped<ValidationControlTypeFactory>();
            container.AddScoped<ValidationTypeDescriptorFactory>();
            container.AddScoped<ValidationTreeResolver>();
        }
    }
}