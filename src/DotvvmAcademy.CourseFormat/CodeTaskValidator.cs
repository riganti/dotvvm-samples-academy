﻿using DotvvmAcademy.Meta;
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

        public CodeTaskValidator(
            CourseWorkspace workspace,
            CSharpValidationService csharpService,
            DothtmlValidationService dothtmlService)
        {
            this.workspace = workspace;
            this.csharpService = csharpService;
            this.dothtmlService = dothtmlService;
            globalProvider = GetServiceProvider();
        }

        public async Task<IUnit> GetUnit(CodeTask codeTask)
        {
            switch (codeTask.CodeLanguage)
            {
                case "csharp":
                    return await GetUnit<CSharpUnit>(codeTask);

                case "dothtml":
                    return await GetUnit<DothtmlUnit>(codeTask);

                default:
                    throw new NotSupportedException($"Code language '{codeTask.CodeLanguage}' is not supported.");
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

        private ScriptOptions GetScriptOptions(CodeTask codeTask)
        {
            return ScriptOptions.Default
                .AddReferences(
                    MetadataReferencer.FromName("mscorlib"),
                    MetadataReferencer.FromName("netstandard"),
                    MetadataReferencer.FromName("System.Private.CoreLib"),
                    MetadataReferencer.FromName("System.Runtime"),
                    MetadataReferencer.FromName("System.Collections"),
                    MetadataReferencer.FromName("System.Reflection"),
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
                .WithFilePath(codeTask.Path)
                .WithSourceResolver(new CodeTaskSourceResolver(workspace));
        }

        private IServiceProvider GetServiceProvider()
        {
            var c = new ServiceCollection();
            c.AddScoped<SourcePathStorage>();
            return c.BuildServiceProvider();
        }

        private Task<IUnit> GetUnit<TUnit>(CodeTask codeTask)
            where TUnit : IUnit
        {
            return cache.GetOrAdd(codeTask.Path, async p =>
            {
                var scope = globalProvider.CreateScope();
                var script = CSharpScript.Create(
                    code: codeTask.Script,
                    options: GetScriptOptions(codeTask),
                    globalsType: typeof(UnitWrapper<TUnit>));
                var unit = (TUnit)ActivatorUtilities.CreateInstance(scope.ServiceProvider, typeof(TUnit));
                await script.RunAsync(new UnitWrapper<TUnit>(unit));
                return unit;
            });
        }
    }
}