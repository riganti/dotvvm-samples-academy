using DotvvmAcademy.Validation;
using DotvvmAcademy.Validation.CSharp;
using DotvvmAcademy.Validation.CSharp.Unit;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Threading.Tasks;

namespace DotvvmAcademy.CourseFormat
{
    internal class CSharpValidationService : IValidationService
    {
        private IServiceProvider services;
        private ICodeTask task;
        private string code;
        private MetadataCollection<MetadataName> staticValidationMetadata;
        private CSharpCompilation compilation;
        private List<ValidationDiagnostic> diagnostics = new List<ValidationDiagnostic>();
        private Assembly userAssembly;
        private SymbolLocator locator;
        private CourseWorkspace workspace;

        public async Task<ImmutableArray<ICodeTaskDiagnostic>> Validate(CourseWorkspace workspace, CodeTaskId id, string code)
        {
            this.workspace = workspace;
            task = await workspace.LoadCodeTask(id);
            this.code = code;
            services = BuildServiceProvider();
            CompileUserCode();
            RunValidationScript();
            await RunValidationAnalyzers();
            if (diagnostics.Count == 0)
            {
                await RewriteAssembly();
            }
            return diagnostics.Select(d =>
            {
                return new CodeTaskDiagnostic
                {
                    Start = d.Location.Start,
                    End = d.Location.End,
                    Severity = CodeTaskDiagnosticSeverity.Error,
                    Message = d.Message
                };
            })
                .Cast<ICodeTaskDiagnostic>()
                .ToImmutableArray();
        }

        private IServiceProvider BuildServiceProvider()
        {
            var collection = new ServiceCollection();
            collection.AddSingleton<MetadataNameFormatter>();
            collection.AddSingleton<ReflectionMetadataNameFormatter>();
            collection.AddSingleton<UserFriendlyMetadataNameFormatter>();
            collection.AddSingleton<IMetadataNameFactory, MetadataNameFactory>();
            collection.AddSingleton<MetadataNameParser>();
            collection.AddSingleton<RoslynMetadataNameProvider>();
            collection.AddSingleton<IAssemblyRewriter, DefaultAssemblyRewriter>();
            collection.AddSingleton<CSharpObject>();
            return collection.BuildServiceProvider();
        }

        private void RunValidationScript()
        {
            var sourceResolver = new CourseFormatSourceResolver(workspace);
            var options = ScriptOptions.Default
                .AddReferences(GetMetadataReference("DotvvmAcademy.Validation.CSharp"))
                .AddImports("DotvvmAcademy.Validation.CSharp", "DotvvmAcademy.Validation.CSharp.Unit")
                .WithFilePath(task.Id.Path)
                .WithSourceResolver(sourceResolver);
            var runner = CSharpScript.Create(
                code: task.ValidationScript,
                options: options,
                globalsType: typeof(ICSharpProject))
                .CreateDelegate();
            var csharpObject = services.GetRequiredService<CSharpObject>();
            runner(csharpObject);
            staticValidationMetadata = csharpObject.GetMetadata();
        }

        private void CompileUserCode()
        {
            compilation = CSharpCompilation.Create(
                assemblyName: $"UserCode_{task.Id.StepId.Moniker}_{Guid.NewGuid()}",
                syntaxTrees: new[] { CSharpSyntaxTree.ParseText(code) },
                references: new[]
                {
                    //TODO: This is not a good practice. It creates a dependency on .NET core.
                    GetMetadataReference("System.Runtime"),
                    GetMetadataReference("System.Collections"),
                    GetMetadataReference("System.Private.CoreLib")
                },
                options: new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));
            locator = ActivatorUtilities.CreateInstance<SymbolLocator>(services, compilation);
        }

        private async Task RunValidationAnalyzers()
        {
            var analyzers = ImmutableArray.Create<DiagnosticAnalyzer>(
                ActivatorUtilities.CreateInstance<BaseTypeAnalyzer>(services, staticValidationMetadata, locator),
                ActivatorUtilities.CreateInstance<DeclarationExistenceAnalyzer>(services, staticValidationMetadata, locator),
                ActivatorUtilities.CreateInstance<InterfaceImplementationAnalyzer>(services, staticValidationMetadata, locator),
                ActivatorUtilities.CreateInstance<SymbolAccessibilityAnalyzer>(services, staticValidationMetadata, locator),
                ActivatorUtilities.CreateInstance<SymbolAllowedAnalyzer>(services, staticValidationMetadata),
                ActivatorUtilities.CreateInstance<SymbolStaticAnalyzer>(services, staticValidationMetadata, locator),
                ActivatorUtilities.CreateInstance<TypeKindAnalyzer>(services, staticValidationMetadata, locator));
            var roslynDiagnostics = await (new CompilationWithAnalyzers(compilation, analyzers, new CompilationWithAnalyzersOptions(
                options: null,
                onAnalyzerException: null,
                concurrentAnalysis: false,
                logAnalyzerExecutionTime: false)))
                .GetAllDiagnosticsAsync();
            diagnostics.AddRange(roslynDiagnostics.Select(d => new RoslynValidationDiagnostic(d)));
        }

        private async Task RewriteAssembly()
        {
            using (var originalStream = new MemoryStream())
            using (var rewrittenStream = new MemoryStream())
            {
                compilation.Emit(originalStream);
                originalStream.Position = 0;
                var rewriter = services.GetRequiredService<IAssemblyRewriter>();
                await rewriter.Rewrite(originalStream, rewrittenStream);
                rewrittenStream.Position = 0;
                userAssembly = AssemblyLoadContext.Default.LoadFromStream(rewrittenStream);
            }
        }

        private void RunDynamicValidation()
        {
            throw new NotImplementedException();
        }

        private MetadataReference GetMetadataReference(string assemblyName)
        {
            return MetadataReference.CreateFromFile(Assembly.Load(assemblyName).Location);
        }

        private TAnalyzer CreateAnalyzer<TAnalyzer>() 
            where TAnalyzer : ValidationDiagnosticAnalyzer
        {
            return ActivatorUtilities.CreateInstance<TAnalyzer>(services, staticValidationMetadata, locator);
        }
    }
}
