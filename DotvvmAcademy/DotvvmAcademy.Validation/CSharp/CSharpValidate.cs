using DotVVM.Framework.Controls;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.Emit;
using Microsoft.CodeAnalysis.Host;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;

namespace DotvvmAcademy.Validation.CSharp
{
    public sealed class CSharpValidate : Validate
    {
        private Lazy<Assembly> assembly;

        public CSharpValidate(MethodInfo validator, string code, IEnumerable<string> dependencies) : base(validator, code, dependencies)
        {
        }

        public CSharpRoot Root { get; private set; }

        public string UserCodeAssemblyName => $"{nameof(DotvvmAcademy)}.UserCode.{Id}";

        internal Assembly Assembly => assembly.Value;

        internal CSharpCompilation Compilation { get; private set; }

        internal CompilationWithAnalyzers CompilationWithAnalyzers { get; set; }

        internal SemanticModel Model { get; private set; }

        internal SyntaxTree Tree { get; private set; }

        public CSharpClass Class(string fullName)
        {
            var descriptor = Descriptor(fullName);
            if (!descriptor.IsActive) return CSharpClass.Inactive;
            return new CSharpClass(this, GetClassDeclaration(descriptor.Symbol));
        }

        public CSharpTypeDescriptor Descriptor(string fullName, params CSharpTypeDescriptor[] genericParameters)
        {
            if (genericParameters.Any(p => !p.IsActive)) return CSharpTypeDescriptor.Inactive;

            var symbol = Compilation.GetTypeByMetadataName(fullName);
            if (symbol == null)
            {
                AddGlobalError($"The compilation is missing the '{fullName}' symbol.");
                return CSharpTypeDescriptor.Inactive;
            }

            return symbol.GetDescriptor(genericParameters);
        }

        public CSharpClassInstance Instance(CSharpClass classObject, params object[] constructorArguments)
        {
            if (!classObject.IsActive) return CSharpClassInstance.Inactive;
            if (ValidationErrors.Count > 0) return CSharpClassInstance.Inactive;
            object instance;
            try
            {
                instance = Assembly.CreateInstance(classObject.Symbol.ToString(), false,
                    BindingFlags.CreateInstance, null, constructorArguments, null, null);
                if (instance == null)
                {
                    classObject.AddError("This class could not be instantiated. Assembly.CreateInstance returned null.");
                    return CSharpClassInstance.Inactive;
                }
            }
            catch (Exception e)
            {
                classObject.AddError($"This class could not be instantiated. Exception message: '{e.Message}'.");
                return CSharpClassInstance.Inactive;
            }

            return new CSharpClassInstance(this, instance);
        }

        protected override void Init()
        {
            try
            {
                var allowedIdentifiers = Validator.GetCustomAttribute<AllowedIdentifiersAttribute>()?.AllowedIdentifiers.ToList();
                allowedIdentifiers.AddRange(GetDefaultAllowedIdentifiers());
                Tree = CSharpSyntaxTree.ParseText(Code);
                var trees = Dependencies.Select(d => CSharpSyntaxTree.ParseText(d)).ToList();
                trees.Add(Tree);
                Compilation = CSharpCompilation.Create(UserCodeAssemblyName, trees, GetMetadataReferences(), GetCompilationOptions());
                var allowedIdentifierAnalyzer = new AllowedIdentifierAnalyzer(GetSymbols(allowedIdentifiers).ToImmutableArray());
                var analyzers = ImmutableArray.Create<DiagnosticAnalyzer>(allowedIdentifierAnalyzer);
                var compilationWithAnalyzersOptions = new CompilationWithAnalyzersOptions(null, OnAnalyzerException, true, true);
                CompilationWithAnalyzers = new CompilationWithAnalyzers(Compilation, analyzers, compilationWithAnalyzersOptions);
                Model = Compilation.GetSemanticModel(Tree);
                assembly = new Lazy<Assembly>(() => EmitToAssembly(Compilation));
                AddErrorsFromDiagnostics(CompilationWithAnalyzers.GetAllDiagnosticsAsync().Result);
                Root = new CSharpRoot(this, Tree.GetCompilationUnitRoot());
            }
            catch (Exception e)
            {
                AddGlobalError($"An exception occurred during C# compilation: '{e}'.");
                Root = CSharpRoot.Inactive;
            }
        }

        private void OnAnalyzerException(Exception exception, DiagnosticAnalyzer analyzer, Diagnostic diagnostic)
        {

        }

        private void AddErrorsFromDiagnostics(IEnumerable<Diagnostic> diagnostics)
        {
            foreach (var diagnostic in diagnostics)
            {
                if (diagnostic.Location.Kind == LocationKind.None)
                {
                    AddGlobalError(diagnostic.ToString());
                }
                else
                {
                    AddError(diagnostic.ToString(), diagnostic.Location.SourceSpan.Start, diagnostic.Location.SourceSpan.End, null);
                }
            }
        }

        private Assembly EmitToAssembly(CSharpCompilation compilation)
        {
            using (var stream = new MemoryStream())
            {
                var result = compilation.Emit(stream);
                if (!result.Success)
                {
                    AddErrorsFromDiagnostics(result.Diagnostics);
                    return null;
                }
                stream.Position = 0;
                return AssemblyLoadContext.Default.LoadFromStream(stream);
            }
        }

        private ClassDeclarationSyntax GetClassDeclaration(ITypeSymbol symbol)
        {
            var declaringReference = symbol.DeclaringSyntaxReferences.Single();
            var node = declaringReference.SyntaxTree.GetRoot().FindNode(declaringReference.Span);
            if (!(node is ClassDeclarationSyntax classDeclaration))
            {
                throw new ArgumentException($"The provided {nameof(ITypeSymbol)} is not a class.");
            }
            return classDeclaration;
        }

        private CSharpCompilationOptions GetCompilationOptions()
        {
            var options = new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary);
            return options;
        }

        private IEnumerable<MetadataReference> GetMetadataReferences()
        {
            var types = new Type[]
            {
                typeof(object),
                typeof(DotvvmControl)
            };

            foreach (var type in types)
            {
                yield return MetadataReference.CreateFromFile(type.Assembly.Location);
            }
        }

        private IEnumerable<string> GetDefaultAllowedIdentifiers()
        {
            return new[]
            {
                typeof(string).FullName,
                typeof(int).FullName,
                typeof(float).FullName,
                typeof(double).FullName,
                typeof(decimal).FullName
            };
        }

        private IEnumerable<ISymbol> GetSymbols(IEnumerable<string> identifiers)
        {
            return Compilation.GetSymbolsWithName(name => identifiers.Contains(name));
        }
    }
}