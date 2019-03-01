using DotvvmAcademy.Meta;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using System;
using System.IO;
using System.Text;
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
            var csharpScript = CSharpScript.Create(
                code: scriptSource,
                options: GetScriptOptions(path));
            var compilation = csharpScript.GetCompilation();
            using (var memoryStream = new MemoryStream())
            {
                var emitResult = compilation.Emit(memoryStream);
                if (!emitResult.Success)
                {
                    var sb = new StringBuilder($"Compilation of a CodeTask at '{path}' failed with the following diagnostics:\n");
                    sb.Append(string.Join(",\n", emitResult.Diagnostics));
                    throw new InvalidOperationException(sb.ToString());
                }
                var entryPoint = compilation.GetEntryPoint(default);
                return new CodeTask(
                    path: path,
                    assembly: memoryStream.ToArray(),
                    entryTypeName: entryPoint.ContainingType.MetadataName,
                    entryMethodName: entryPoint.MetadataName);
            }
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
                    RoslynReference.FromName("DotvvmAcademy.Validation"),
                    RoslynReference.FromName("DotvvmAcademy.Validation.CSharp"),
                    RoslynReference.FromName("DotvvmAcademy.Validation.Dothtml"),
                    RoslynReference.FromName("DotvvmAcademy.Meta"))
                .WithFilePath(scriptPath)
                .WithSourceResolver(new CourseSourceReferenceResolver(environment));
        }
    }
}