using System;
using System.Collections.Immutable;
using System.Threading.Tasks;

namespace DotvvmAcademy.CourseFormat
{
    public class ValidationService : IValidationService
    {
        private readonly CSharpValidationService csharp = new CSharpValidationService();
        private readonly DothtmlValidationService dothtml = new DothtmlValidationService();

        public Task<ImmutableArray<ICodeTaskDiagnostic>> Validate(ICodeTask task, string code)
        {
            switch (task.Id.Language)
            {
                case ".cs":
                    return csharp.Validate(task, code);

                case ".dothtml":
                    return dothtml.Validate(task, code);

                default:
                    throw new ArgumentException($"The programming language of {nameof(CodeTask)} '{task.Id.Path}'" +
                        $"is not supported by {nameof(ValidationService)}.", nameof(task));
            }
        }
    }
}