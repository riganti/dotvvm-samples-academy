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
            var configuration = unit.Provider.GetRequiredService<CodeTaskConfiguration>();
            var sourceCodeTasks = configuration.SourcePaths.Select(p => GetSourceCode(p.Key, p.Value));
            var sourceCodes = (await Task.WhenAll(sourceCodeTasks)).ToImmutableArray();
            switch (unit)
            {
                case CSharpUnit csharpUnit:
                    sourceCodes = sourceCodes.Add(new CSharpSourceCode(code, configuration.FileName, true));
                    diagnostics = await csharpService.Validate(csharpUnit, sourceCodes);
                    break;

                case DothtmlUnit dothtmlUnit:
                    sourceCodes = sourceCodes.Add(new DothtmlSourceCode(code, configuration.FileName, true));
                    diagnostics = await dothtmlService.Validate(dothtmlUnit, sourceCodes);
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

        private async Task<ISourceCode> GetSourceCode(string fileName, string sourcePath)
        {
            var resource = await workspace.Require<Resource>(sourcePath);
            // TODO: Judging file type merely by extension is not exactly great
            var extension = SourcePath.GetExtension(resource.Path).ToString();
            switch (extension)
            {
                case ".cs":
                    return new CSharpSourceCode(resource.Text, fileName, false);

                case ".dothtml":
                    return new DothtmlSourceCode(resource.Text, fileName, false);

                default:
                    throw new NotSupportedException($"File extension '{extension}' is not supported.");
            }
        }
    }
}