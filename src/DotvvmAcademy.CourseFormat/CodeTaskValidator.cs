using DotvvmAcademy.Validation;
using DotvvmAcademy.Validation.CSharp;
using DotvvmAcademy.Validation.CSharp.Unit;
using DotvvmAcademy.Validation.Dothtml;
using DotvvmAcademy.Validation.Dothtml.Unit;
using DotvvmAcademy.Validation.Unit;
using System;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;

namespace DotvvmAcademy.CourseFormat
{
    public class CodeTaskValidator
    {
        private readonly CSharpValidationService csharpService;
        private readonly DothtmlValidationService dothtmlService;
        private readonly CourseWorkspace workspace;

        public CodeTaskValidator(
            CourseWorkspace workspace,
            CSharpValidationService csharpService,
            DothtmlValidationService dothtmlService)
        {
            this.workspace = workspace;
            this.csharpService = csharpService;
            this.dothtmlService = dothtmlService;
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
                    var viewModelPath = dothtmlUnit.GetViewModelPath();
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
    }
}