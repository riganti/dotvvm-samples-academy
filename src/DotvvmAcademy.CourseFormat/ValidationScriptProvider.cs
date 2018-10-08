using DotvvmAcademy.Meta;
using DotvvmAcademy.Validation.CSharp.Unit;
using DotvvmAcademy.Validation.Dothtml.Unit;
using DotvvmAcademy.Validation.Unit;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace DotvvmAcademy.CourseFormat
{
    public class ValidationScriptProvider : ISourceProvider<ValidationScript>
    {
        private readonly ICourseEnvironment environment;
        private readonly IServiceProvider globalProvider;

        public ValidationScriptProvider(ICourseEnvironment environment)
        {
            this.environment = environment;
            globalProvider = GetServiceProvider();
        }

        public Task<ValidationScript> Get(string path)
        {
            var language = SourcePath.GetValidatedLanguage(path);
            // TODO: Make extensible (remove switch)
            switch (language)
            {
                case ValidatedLanguages.CSharp:
                    return Get<CSharpUnit>(path);

                case ValidatedLanguages.Dothtml:
                    return Get<DothtmlUnit>(path);

                default:
                    throw new NotSupportedException($"Validated language '{language}' is not supported.");
            }
        }

        public async Task<ValidationScript> Get<TUnit>(string path)
            where TUnit : IValidationUnit
        {
            var scriptSource = await environment.Read(path);
            var scope = globalProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<Context>();
            context.ScriptPath = path;
            var csharpScript = CSharpScript.Create(
                code: scriptSource,
                options: GetScriptOptions(path),
                globalsType: typeof(UnitWrapper<TUnit>));
            var unit = scope.ServiceProvider.GetRequiredService<TUnit>();
            await csharpScript.RunAsync(new UnitWrapper<TUnit>(unit));
            return new ValidationScript(path, unit);
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
                .AddImports(
                    "DotvvmAcademy.Validation.Unit",
                    "DotvvmAcademy.Validation.CSharp.Unit",
                    "DotvvmAcademy.Validation.Dothtml.Unit",
                    "DotvvmAcademy.Meta",
                    "DotVVM.Framework.Controls",
                    "DotVVM.Framework.Controls.Infrastructure",
                    "System",
                    "System.Collections.Generic",
                    "System.Linq",
                    "System.Text",
                    "System.Threading.Tasks")
                .WithFilePath(scriptPath)
                .WithSourceResolver(new CourseSourceReferenceResolver(environment));
        }

        private IServiceProvider GetServiceProvider()
        {
            var c = new ServiceCollection();
            c.AddMeta();
            c.AddScoped(p =>
            {
                var context = p.GetRequiredService<Context>();
                return new CodeTaskOptions(context.ScriptPath);
            });
            c.AddScoped<Context>();
            c.AddScoped<DothtmlUnit>();
            c.AddScoped<CSharpUnit>();
            return c.BuildServiceProvider();
        }

        private class Context
        {
            public string ScriptPath { get; set; }
        }
    }
}