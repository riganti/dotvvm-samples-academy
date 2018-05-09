using DotvvmAcademy.Validation;
using DotvvmAcademy.Validation.CSharp;
using DotvvmAcademy.Validation.CSharp.Unit;
using DotvvmAcademy.Validation.Dothtml.Unit;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace DotvvmAcademy.CourseFormat
{
    internal class ValidationService
    {
        public Task<ImmutableArray<ICodeTaskDiagnostic>> Validate(CodeTask task, string userCode)
        {
            var globalsType = task.Id.Language == "cs" ? typeof(ICSharpProject) : typeof(IDothtmlView);
            var script = CSharpScript.Create(task.ValidationScript, globalsType: globalsType);
            var runner = script.CreateDelegate();
            return task.Id.Language == "cs" ? ValidateCSharp(runner, userCode) : ValidateDothtml(runner, userCode);
        }


        private MetadataReference GetMetadataReference(string assemblyName)
        {
            return MetadataReference.CreateFromFile(Assembly.Load(assemblyName).Location);
        }

        private async Task<ImmutableArray<ICodeTaskDiagnostic>> ValidateDothtml(ScriptRunner<object> runner, string userCode)
        {

        }

        private async Task<ImmutableArray<ICodeTaskDiagnostic>> ValidateCSharp(ScriptRunner<object> runner, string userCode)
        {
            var csharpObject = new CSharpObject();
            await runner(csharpObject);
            var metadata = csharpObject.GetMetadata();
            var tree = CSharpSyntaxTree.ParseText(userCode);
            var references = new[]
            {
                GetMetadataReference("System.Runtime"),
                GetMetadataReference("System.Colletions.Generic"),
                GetMetadataReference("System.Private.CoreLib"),
            };
            var compilation = CSharpCompilation.Create($"UserCode_{Guid.NewGuid()}", new[] { tree }, references, new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary, true));
            var collection = new ServiceCollection();
            collection.AddSingleton(metadata);
            collection.AddSingleton(compilation);
            collection.AddSingleton<RoslynMetadataNameProvider>();
            collection.AddSingleton<IMetadataNameFactory, MetadataNameFactory>();
            collection.AddSingleton<MetadataNameFormatter>();
            collection.AddSingleton<ReflectionMetadataNameFormatter>();
            collection.AddSingleton<UserFriendlyMetadataNameFormatter>();
            collection.AddSingleton<SymbolLocator>();
            collection.AddSingleton<ValidationDiagnosticAnalyzer, BaseTypeAnalyzer>();
            collection.AddSingleton<ValidationDiagnosticAnalyzer, DeclarationExistenceAnalyzer>();
            collection.AddSingleton<ValidationDiagnosticAnalyzer, InterfaceImplementationAnalyzer>();
            collection.AddSingleton<ValidationDiagnosticAnalyzer, SymbolAccessibilityAnalyzer>();
            collection.AddSingleton<ValidationDiagnosticAnalyzer, SymbolAllowedAnalyzer>();
            collection.AddSingleton<ValidationDiagnosticAnalyzer, SymbolStaticAnalyzer>();
            collection.AddSingleton<ValidationDiagnosticAnalyzer, TypeKindAnalyzer>();
            var provider = collection.BuildServiceProvider();
            var with = new CompilationWithAnalyzers(compilation, provider.GetService<IEnumerable<ValidationDiagnosticAnalyzer>>().Cast<DiagnosticAnalyzer>().ToImmutableArray(), new CompilationWithAnalyzersOptions(null, null, false, false));
            return (await with.GetAllDiagnosticsAsync()).Select(d => ConvertDiagnostic(new RoslynValidationDiagnostic(d))).ToImmutableArray();
        }

        private ICodeTaskDiagnostic ConvertDiagnostic(ValidationDiagnostic validation)
        {
            return new CodeTaskDiagnostic
            {
                Start = validation.Location.Start,
                End = validation.Location.End,
                Severity = CodeTaskDiagnosticSeverity.Error
            };
        }
    }
}