using DotvvmAcademy.Validation;
using DotvvmAcademy.Validation.CSharp;
using DotvvmAcademy.Validation.CSharp.Unit;
using DotvvmAcademy.Validation.Dothtml;
using DotvvmAcademy.Validation.Dothtml.Unit;
using DotvvmAcademy.Validation.Unit;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;

namespace DotvvmAcademy.CourseFormat
{
    public class CodeTaskValidator
    {
        private readonly CSharpValidationService csharpValidationService;
        private readonly DothtmlValidationService dothtmlValidationService;
        private readonly ICourseEnvironment environment;

        public CodeTaskValidator(
            ICourseEnvironment environment,
            CSharpValidationService csharpValidationService,
            DothtmlValidationService dothtmlValidationService)
        {
            this.environment = environment;
            this.csharpValidationService = csharpValidationService;
            this.dothtmlValidationService = dothtmlValidationService;
        }

        public async Task<IEnumerable<CodeTaskDiagnostic>> Validate(CodeTask codeTask, string code)
        {
            var absolutePath = SourcePath.Combine(SourcePath.GetParent(codeTask.Path), codeTask.Unit.GetDefault());
            var sources = (await Task.WhenAll(codeTask.Unit.GetDependencies()
                .Select(async p => (path: SourcePath.Combine(SourcePath.GetParent(codeTask.Path), p), content: await environment.Read(p)))))
                .Select(t => CreateSourceCode(t.path, t.content, false))
                .ToImmutableArray()
                .Add(CreateSourceCode(absolutePath, code, true));
            var service = GetValidationService(codeTask.Unit);
            return (await service.Validate(codeTask.Unit.GetConstraints(), sources))
                .Select(d => new CodeTaskDiagnostic(
                    message: string.Format(d.Message, d.Arguments.ToArray()),
                    start: d.Start,
                    end: d.End,
                    severity: d.Severity.ToCodeTaskDiagnosticSeverity()))
                .ToImmutableArray();
        }

        private ISourceCode CreateSourceCode(string fileName, string source, bool isValidated)
        {
            // TODO: Judging file type merely by extension is not exactly great
            var extension = SourcePath.GetExtension(fileName).ToString();
            switch (extension)
            {
                case ".cs":
                    return new CSharpSourceCode(source, fileName, isValidated);

                case ".dothtml":
                    return new DothtmlSourceCode(source, fileName, isValidated);

                default:
                    throw new NotSupportedException($"File extension '{extension}' is not supported.");
            }
        }

        private IValidationService GetValidationService(IValidationUnit unit)
        {
            switch (unit)
            {
                case CSharpUnit csharpUnit:
                    return csharpValidationService;

                case DothtmlUnit dothmlUnit:
                    return dothtmlValidationService;

                default:
                    throw new NotSupportedException($"IValidationUnit type \"{unit.GetType()}\" is not supported.");
            }
        }
    }
}