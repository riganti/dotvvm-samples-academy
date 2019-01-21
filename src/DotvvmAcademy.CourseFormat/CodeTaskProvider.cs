using DotvvmAcademy.Meta;
using DotvvmAcademy.Validation.Unit;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using System.Threading.Tasks;

namespace DotvvmAcademy.CourseFormat
{
    public class CodeTaskProvider : ISourceProvider<CodeTask>
    {
        private readonly ICourseEnvironment environment;

        public CodeTaskProvider(ICourseEnvironment environment)
        {
            this.environment = environment;
        }

        public async Task<CodeTask> Get(string path)
        {
            var scriptSource = await environment.Read(path);
            var csharpScript = CSharpScript.Create<IValidationUnit>(
                code: scriptSource,
                options: GetScriptOptions(path));
            csharpScript = csharpScript.ContinueWith<IValidationUnit>("return Unit;");
            var state = await csharpScript.RunAsync();
            if (state.Exception != null)
            {
                throw new CodeTaskCompilationException(state.Exception);
            }
            return new CodeTask(path, state.ReturnValue);
        }

        private ScriptOptions GetScriptOptions(string scriptPath)
        {
            return ScriptOptions.Default
                .AddReferences(
                    RoslynReference.FromName("netstandard"),
                    RoslynReference.FromName("System.Private.CoreLib"),
                    RoslynReference.FromName("System.Runtime"),
                    RoslynReference.FromName("System.Collections"),
                    RoslynReference.FromName("System.Reflection"),
                    RoslynReference.FromName("System.ComponentModel.Annotations"),
                    RoslynReference.FromName("System.ComponentModel.DataAnnotations"),
                    RoslynReference.FromName("System.Linq"),
                    RoslynReference.FromName("System.Linq.Expressions"), // Roslyn #23573
                    RoslynReference.FromName("Microsoft.CSharp"), // Roslyn #23573
                    RoslynReference.FromName("DotVVM.Framework"),
                    RoslynReference.FromName("DotVVM.Core"),
                    RoslynReference.FromName("DotvvmAcademy.CourseFormat"),
                    RoslynReference.FromName("DotvvmAcademy.Validation"),
                    RoslynReference.FromName("DotvvmAcademy.Validation.CSharp"),
                    RoslynReference.FromName("DotvvmAcademy.Validation.Dothtml"),
                    RoslynReference.FromName("DotvvmAcademy.Meta"))
                .WithFilePath(scriptPath)
                .WithSourceResolver(new CourseSourceReferenceResolver(environment));
        }
    }
}