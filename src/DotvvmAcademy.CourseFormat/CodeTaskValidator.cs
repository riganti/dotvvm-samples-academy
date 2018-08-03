using DotvvmAcademy.Meta;
using DotvvmAcademy.Validation;
using DotvvmAcademy.Validation.CSharp;
using DotvvmAcademy.Validation.CSharp.Unit;
using DotvvmAcademy.Validation.Dothtml;
using DotvvmAcademy.Validation.Dothtml.Unit;
using DotvvmAcademy.Validation.Unit;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Concurrent;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;

namespace DotvvmAcademy.CourseFormat
{
    public class CodeTaskValidator
    {
        private readonly ConcurrentDictionary<string, Task<IUnit>> cache
            = new ConcurrentDictionary<string, Task<IUnit>>();

        private readonly CSharpValidationService csharpService;
        private readonly DothtmlValidationService dothtmlService;
        private readonly IServiceProvider globalProvider;
        private readonly CourseWorkspace workspace;
        private readonly CourseEnvironment environment;

        public CodeTaskValidator(
            CourseWorkspace workspace,
            CourseEnvironment environment,
            CSharpValidationService csharpService,
            DothtmlValidationService dothtmlService)
        {
            this.workspace = workspace;
            this.environment = environment;
            this.csharpService = csharpService;
            this.dothtmlService = dothtmlService;
            globalProvider = GetServiceProvider();
        }

        public async Task<IUnit> GetUnit(string codeTaskPath)
        {
            var script = await workspace.Load<Resource>(codeTaskPath);
            var language = SourcePath.GetCodeLanguage(script.Path);
            switch (language)
            {
                case "csharp":
                    return await RunScript<CSharpUnit>(script);

                case "dothtml":
                    return await RunScript<DothtmlUnit>(script);

                default:
                    throw new NotSupportedException($"Code language '{language}' is not supported.");
            }
        }

        public async Task<ImmutableArray<CodeTaskDiagnostic>> Validate(IUnit unit, string code)
        {
            ImmutableArray<IValidationDiagnostic> diagnostics;
            switch (unit)
            {
                case CSharpUnit csharpUnit:
                    diagnostics = await csharpService.Validate(csharpUnit, code);
                    break;

                case DothtmlUnit dothtmlUnit:
                    var viewModel = string.Empty;
                    var viewModelPath = dothtmlUnit.Provider.GetRequiredService<SourcePathStorage>().Get("ViewModel");
                    if (viewModelPath != null)
                    {
                        viewModel = (await workspace.Load<Resource>(viewModelPath)).Text;
                    }
                    var options = new DothtmlValidationOptions(viewModel: viewModel);
                    diagnostics = await dothtmlService.Validate(dothtmlUnit, code, options);
                    break;

                default:
                    throw new NotSupportedException($"{nameof(IUnit)} type '{unit.GetType().Name}' is not supported.");
            }

            return diagnostics.Select(d => new CodeTaskDiagnostic(
                message: d.Message,
                start: d.Start,
                end: d.End,
                severity: d.Severity.ToCodeTaskDiagnosticSeverity()))
                .ToImmutableArray();
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
            return c.BuildServiceProvider();
        }

        private Task<IUnit> RunScript<TUnit>(Resource script)
            where TUnit : IUnit
        {
            return cache.GetOrAdd(script.Path, async p =>
            {
                var scope = globalProvider.CreateScope();
                var csharpScript = CSharpScript.Create(
                    code: script.Text,
                    options: GetScriptOptions(script),
                    globalsType: typeof(UnitWrapper<TUnit>));
                var unit = (TUnit)ActivatorUtilities.CreateInstance(scope.ServiceProvider, typeof(TUnit));
                await csharpScript.RunAsync(new UnitWrapper<TUnit>(unit));
                return unit;
            });
        }
    }
}