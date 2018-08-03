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
                context.ScriptPath = script.Path;
                var csharpScript = CSharpScript.Create(
                    code: script.Text,
                    options: GetScriptOptions(script),
                    globalsType: typeof(UnitWrapper<TUnit>));
                var unit = (TUnit)ActivatorUtilities.CreateInstance(scope.ServiceProvider, typeof(TUnit));
                await csharpScript.RunAsync(new UnitWrapper<TUnit>(unit));
                return unit;
            });
        }

        private ScriptOptions GetScriptOptions(Resource script)
        {
            return ScriptOptions.Default
                .AddReferences(
                    MetadataReferencer.FromName("netstandard"),
                    MetadataReferencer.FromName("System.Private.CoreLib"),
                    MetadataReferencer.FromName("System.Runtime"),
                    MetadataReferencer.FromName("System.Collections"),
                    MetadataReferencer.FromName("System.Reflection"),
                    MetadataReferencer.FromName("System.Linq"),
                    MetadataReferencer.FromName("System.Linq.Expressions"), // Roslyn #23573
                    MetadataReferencer.FromName("Microsoft.CSharp"), // Roslyn #23573
                    MetadataReferencer.FromName("DotVVM.Framework"),
                    MetadataReferencer.FromName("DotVVM.Core"),
                    MetadataReferencer.FromName("DotvvmAcademy.CourseFormat"),
                    MetadataReferencer.FromName("DotvvmAcademy.Validation"),
                    MetadataReferencer.FromName("DotvvmAcademy.Validation.CSharp"),
                    MetadataReferencer.FromName("DotvvmAcademy.Validation.Dothtml"))
                .AddImports(
                    "System",
                    "DotvvmAcademy.Validation.Unit",
                    "DotvvmAcademy.Validation.CSharp.Unit",
                    "DotvvmAcademy.Validation.Dothtml.Unit")
                .WithFilePath(script.Path)
                .WithSourceResolver(new CodeTaskSourceResolver(environment));
        }

        private IServiceProvider GetServiceProvider()
        {
            var c = new ServiceCollection();
            c.AddScoped<SourcePathStorage>();
            c.AddScoped<Context>();
            return c.BuildServiceProvider();
        }

        public class Context
        {
            public string ScriptPath { get; set; }
        }
    }
}