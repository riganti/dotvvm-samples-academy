using DotvvmAcademy.Meta;
using DotvvmAcademy.Validation;
using DotvvmAcademy.Validation.CSharp;
using DotvvmAcademy.Validation.CSharp.Unit;
using DotvvmAcademy.Validation.Dothtml;
using DotvvmAcademy.Validation.Dothtml.Unit;
using DotvvmAcademy.Validation.Unit;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using System;
using System.Collections.Concurrent;
using System.Collections.Immutable;
using System.Threading.Tasks;

namespace DotvvmAcademy.CourseFormat
{
    public class CodeTaskValidator
    {
        private readonly ConcurrentDictionary<string, Task<IUnit>> cache
            = new ConcurrentDictionary<string, Task<IUnit>>();

        private readonly IValidationService<CSharpUnit, CSharpValidationOptions> csharpService;
        private readonly IValidationService<DothtmlUnit, DothtmlValidationOptions> dothtmlService;
        private readonly CourseWorkspace workspace;

        public CodeTaskValidator(
            CourseWorkspace workspace,
            IValidationService<CSharpUnit, CSharpValidationOptions> csharpService,
            IValidationService<DothtmlUnit, DothtmlValidationOptions> dothtmlService)
        {
            this.workspace = workspace;
            this.csharpService = csharpService;
            this.dothtmlService = dothtmlService;
        }

        public Task<IUnit> GetUnit(CodeTask codeTask)
        {
            return cache.GetOrAdd(codeTask.Path, async p =>
            {
                var globalsType = GetGlobalsType(codeTask);
                var script = CSharpScript.Create(
                    code: codeTask.Script,
                    options: GetScriptOptions(codeTask),
                    globalsType: GetGlobalsType(codeTask));
                var unit = (IUnit)Activator.CreateInstance(GetGlobalsType(codeTask));
                await script.RunAsync(unit);
                return unit;
            });
        }

        public Task<ImmutableArray<IValidationDiagnostic>> Validate(IUnit unit, string code)
        {
            switch (unit)
            {
                case CSharpUnit csharpUnit:
                    return csharpService.Validate(csharpUnit, code);

                case DothtmlUnit dothtmlUnit:
                    return dothtmlService.Validate(dothtmlUnit, code);

                default:
                    throw new NotSupportedException($"{nameof(IUnit)} type '{unit.GetType().Name}' is not supported.");
            }
        }

        private Type GetGlobalsType(CodeTask codeTask)
        {
            switch (codeTask.CodeLanguage)
            {
                case "csharp":
                    return typeof(CSharpUnit);

                case "dothtml":
                    return typeof(DothtmlUnit);

                default:
                    throw new NotSupportedException($"Code language '{codeTask.CodeLanguage}' is not supported.");
            }
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
    }
}