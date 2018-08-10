using DotvvmAcademy.Validation;
using DotvvmAcademy.Validation.CSharp;
using DotvvmAcademy.Validation.CSharp.Unit;
using DotvvmAcademy.Validation.Dothtml;
using DotvvmAcademy.Validation.Dothtml.Unit;
using DotvvmAcademy.Validation.Unit;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
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
            var sourceCodes = ImmutableArray.CreateBuilder<ISourceCode>();
            sourceCodes.AddRange(await LoadSourceCodes(unit));
            switch (unit)
            {
                case CSharpUnit csharpUnit:
                    sourceCodes.Add(new CSharpSourceCode(code));
                    diagnostics = await csharpService.Validate(csharpUnit, sourceCodes.ToImmutable());
                    break;

                case DothtmlUnit dothtmlUnit:
                    sourceCodes.Add(new DothtmlSourceCode(code));
                    diagnostics = await dothtmlService.Validate(dothtmlUnit, sourceCodes.ToImmutable());
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

        private async Task<IEnumerable<ISourceCode>> LoadSourceCodes(IUnit unit)
        {
            var sourcePaths = unit.Provider.GetRequiredService<SourcePathStorage>().GetSourcePaths();
            var resources = await Task.WhenAll(sourcePaths.Select(p => workspace.Load<Resource>(p)));
            return resources.Select(GetSourceCode);
        }

        private ISourceCode GetSourceCode(Resource resource)
        {
            // TODO: Judging file type merely by extension is not exactly great
            var extension = SourcePath.GetExtension(resource.Path).ToString();
            switch (extension)
            {
                case ".cs":
                    return new CSharpSourceCode(resource.Text);

                case ".dothtml":
                    return new DothtmlSourceCode(resource.Text);

                default:
                    throw new NotSupportedException($"Extension '{extension}' is not supported.");
            }
        }
    }
}