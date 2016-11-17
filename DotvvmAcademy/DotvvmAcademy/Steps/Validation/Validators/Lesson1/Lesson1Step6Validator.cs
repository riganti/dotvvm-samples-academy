using DotvvmAcademy.Steps.Validation.Interfaces;
using DotVVM.Framework.Compilation.ControlTree.Resolved;

namespace DotvvmAcademy.Steps.Validation.Validators.Lesson1
{
    [StepValidation(ValidationKey = "Lesson1Step6Validator")]
    public class Lesson1Step6Validator : IDotHtmlCodeStepValidationObject
    {
        public void Validate(ResolvedTreeRoot resolvedTreeRoot)
        {
            ValidatorHelper.ValidateTextBoxBindings(resolvedTreeRoot);
        }
    }
}