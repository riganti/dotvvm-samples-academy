using System;
using System.Collections.Immutable;
using System.Threading.Tasks;

namespace DotvvmAcademy.CourseFormat
{
    public class ValidationService : IValidationService
    {
        private readonly CSharpValidationService csharp;
        private readonly DothtmlValidationService dothtml;

        public ValidationService(CourseWorkspace workspace)
        {
            csharp = new CSharpValidationService(workspace);
            dothtml = new DothtmlValidationService(workspace);
        }

        public Task<ImmutableArray<ICodeTaskDiagnostic>> Validate(CodeTaskId id, string code)
        {
            switch (id.Language)
            {
                case ".cs":
                    return csharp.Validate(id, code);

                case ".dothtml":
                    return dothtml.Validate(id, code);

                default:
                    throw new ArgumentException($"The programming language of {nameof(CodeTask)} '{id.Path}'" +
                        $"is not supported by {nameof(ValidationService)}.", nameof(id));
            }
        }
    }
}