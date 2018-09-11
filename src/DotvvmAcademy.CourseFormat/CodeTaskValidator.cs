using DotvvmAcademy.Validation;
using DotvvmAcademy.Validation.CSharp;
using DotvvmAcademy.Validation.Dothtml;
using DotvvmAcademy.Validation.Unit;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;

namespace DotvvmAcademy.CourseFormat
{
    public class CodeTaskValidator
    {
        private readonly ICourseEnvironment environment;
        private readonly IServiceProvider provider;
        private readonly CourseWorkspace workspace;

        public CodeTaskValidator(
            ICourseEnvironment environment,
            CourseWorkspace workspace,
            IServiceProvider provider)
        {
            this.environment = environment;
            this.workspace = workspace;
            this.provider = provider;
        }

        public async Task<ImmutableArray<CodeTaskDiagnostic>> Validate(IUnit unit, string code)
        {
            var configuration = unit.Provider.GetRequiredService<CodeTaskOptions>();
            var sourceCodeTasks = configuration.SourcePaths.Select(async p => (fileName: p.Key, source: await environment.Read(p.Value)));
            var sourceCodes = (await Task.WhenAll(sourceCodeTasks))
                .Select(t => CreateSourceCode(t.fileName, t.source, false))
                .ToImmutableArray()
                .Add(CreateSourceCode(configuration.FileName, code, true));
            var validationServiceType = typeof(IValidationService<>).MakeGenericType(unit.GetType());
            var validationService = (IValidationService)provider.GetRequiredService(validationServiceType);
            var diagnostics = await validationService.Validate(unit, sourceCodes);
            return diagnostics.Select(d =>
                new CodeTaskDiagnostic(
                    message: d.Message,
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
    }
}