using DotvvmAcademy.Meta;
using DotvvmAcademy.Validation.CSharp.Unit;
using DotvvmAcademy.Validation.Dothtml.Unit;
using DotvvmAcademy.Validation.Unit;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace DotvvmAcademy.CourseFormat
{
    public class ValidationScriptRunner
    {
        private readonly CourseEnvironment environment;
        private readonly IServiceProvider globalProvider;

        private readonly ConcurrentDictionary<string, Task<IUnit>> units
            = new ConcurrentDictionary<string, Task<IUnit>>();

        private readonly CourseWorkspace workspace;

        public ValidationScriptRunner(CourseWorkspace workspace, CourseEnvironment environment)
        {
            this.workspace = workspace;
            this.environment = environment;
            globalProvider = GetServiceProvider();
        }

        public async Task<IUnit> Run(string scriptPath)
        {
            var script = await workspace.Load<Resource>(scriptPath);
            return await Run(script);
        }

        public Task<IUnit> Run(Resource script)
        {
            var language = SourcePath.GetValidatedLanguage(script.Path);
            switch (language)
            {
                case ValidatedLanguages.CSharp:
                    return Run<CSharpUnit>(script);

                case ValidatedLanguages.Dothtml:
                    return Run<DothtmlUnit>(script);

                default:
                    throw new NotSupportedException($"Validated language '{language}' is not supported.");
            }
        }

        public Task<IUnit> Run<TUnit>(Resource script)
            where TUnit : IUnit
        {
            return units.GetOrAdd(script.Path, async p =>
            {
                var scope = globalProvider.CreateScope();
                var context = scope.ServiceProvider.GetRequiredService<Context>();
                context.Script = script;
                var csharpScript = CSharpScript.Create(
                    code: script.Text,
                    options: GetScriptOptions(script),
                    globalsType: typeof(UnitWrapper<TUnit>));
                var unit = scope.ServiceProvider.GetRequiredService<TUnit>();
                await csharpScript.RunAsync(new UnitWrapper<TUnit>(unit));
                return unit;
            });
        }

        private ScriptOptions GetScriptOptions(Resource script)
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
                .WithFilePath(script.Path)
                .WithSourceResolver(new CodeTaskSourceResolver(environment));
        }

        private IServiceProvider GetServiceProvider()
        {
            var c = new ServiceCollection();
            c.AddMetaScopeFriendly();
            c.AddScoped(p =>
            {
                var context = p.GetRequiredService<Context>();
                var scriptDirectory = SourcePath.GetParent(context.Script.Path);
                return new SourcePathStorage(scriptDirectory);
            });
            c.AddScoped(p =>
            {
                var context = p.GetRequiredService<Context>();
                return new CodeTaskConfiguration(context.Script.Path);
            });
            c.AddScoped<Context>();
            c.AddScoped<DothtmlUnit>();
            c.AddScoped<CSharpUnit>();
            return c.BuildServiceProvider();
        }

        private class Context
        {
            public Resource Script { get; set; }
        }
    }
}