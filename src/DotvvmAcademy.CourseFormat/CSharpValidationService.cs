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
        public static ValidationDiagnosticDescriptor DynamicValidationExceptionError = new ValidationDiagnosticDescriptor(
                id: "DYNEX",
                name: "Dynamic Validation Exception Error",
                message: "An exception has been thrown: '{0}'",
                severity: ValidationDiagnosticSeverity.Error);

        private List<DynamicValidationAction> actions;
        private string code;
        private CSharpCompilation compilation;
        private List<ValidationDiagnostic> diagnostics = new List<ValidationDiagnostic>();
        private SymbolLocator locator;
        private IServiceProvider services;
        private MetadataCollection<MetadataName> staticValidationMetadata;
        private ICodeTask task;
        private Assembly userAssembly;
        private CourseWorkspace workspace;

        public CSharpValidationService(CourseWorkspace workspace)
        {
            this.workspace = workspace;
        }

        public async Task<ImmutableArray<ICodeTaskDiagnostic>> Validate(CodeTaskId id, string code)
        {
            diagnostics.Clear();
            task = await workspace.LoadCodeTask(id);
            this.code = code;
            services = BuildServiceProvider();
            CompileUserCode();
            RunValidationScript();
            await RunValidationAnalyzers();
            if (diagnostics.Count == 0)
            {
                await RewriteAssembly();
                RunDynamicValidation();
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
            collection.AddSingleton<ReflectionMetadataNameProvider>();
            collection.AddSingleton<IAssemblyRewriter, DefaultAssemblyRewriter>();
            collection.AddSingleton<CSharpObject>();
            return collection.BuildServiceProvider();
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

        private TAnalyzer CreateAnalyzer<TAnalyzer>()
            where TAnalyzer : ValidationDiagnosticAnalyzer
        {
            return ActivatorUtilities.CreateInstance<TAnalyzer>(services, staticValidationMetadata, locator);
        }

        private MetadataReference GetMetadataReference(string assemblyName)
        {
            return MetadataReference.CreateFromFile(Assembly.Load(assemblyName).Location);
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
            var infoLocator = ActivatorUtilities.CreateInstance<MemberInfoLocator>(services, userAssembly);
            var context = new DynamicValidationContext(infoLocator);
            foreach (var action in actions)
            {
                try
                {
                    action(context);
                }
                catch (Exception e)
                {
                    diagnostics.Add(new ExceptionValidationDiagnostic(DynamicValidationExceptionError, e, e.Message));
                }
            }
            diagnostics.AddRange(context.Diagnostics);
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
            var options = new CompilationWithAnalyzersOptions(
                options: null,
                onAnalyzerException: null,
                concurrentAnalysis: false,
                logAnalyzerExecutionTime: false);
            var compilationWithAnalyzers = new CompilationWithAnalyzers(compilation, analyzers, options);
            var roslynDiagnostics = await compilationWithAnalyzers.GetAllDiagnosticsAsync();
            diagnostics.AddRange(roslynDiagnostics.Select(d => new RoslynValidationDiagnostic(d)));
        }

        private void RunValidationScript()
        {
            var sourceResolver = new CourseFormatSourceResolver(workspace);
            var options = ScriptOptions.Default
                .AddReferences(
                    GetMetadataReference("DotvvmAcademy.Validation.CSharp"),
                    GetMetadataReference("Microsoft.CSharp"),
                    GetMetadataReference("System.Runtime"),
                    GetMetadataReference("System.Private.CoreLib"),
                    GetMetadataReference("System.Linq.Expressions")) // Roslyn Issue #23573
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
            actions = csharpObject.Actions;
        }
    }
}