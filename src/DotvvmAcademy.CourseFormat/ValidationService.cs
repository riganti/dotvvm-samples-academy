using System;
using System.Collections.Immutable;
using System.Threading.Tasks;

namespace DotvvmAcademy.CourseFormat
{
    public class ValidationService : IValidationService
    {
        private readonly CSharpValidationService csharp = new CSharpValidationService();
        private readonly DothtmlValidationService dothtml = new DothtmlValidationService();

        public Task<ImmutableArray<ICodeTaskDiagnostic>> Validate(CourseWorkspace workspace, CodeTaskId id, string code)
        {
            switch (id.Language)
            {
                case ".cs":
                    return csharp.Validate(workspace, id, code);

                case ".dothtml":
                    return dothtml.Validate(workspace, id, code);

                default:
                    throw new ArgumentException($"The programming language of {nameof(CodeTask)} '{id.Path}'" +
                        $"is not supported by {nameof(ValidationService)}.", nameof(id));
            }
        }
    }
}