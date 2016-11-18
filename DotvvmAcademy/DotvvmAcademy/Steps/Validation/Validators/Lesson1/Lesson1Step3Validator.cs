using DotvvmAcademy.Steps.Validation.Interfaces;
using DotVVM.Framework.Compilation.ControlTree.Resolved;

namespace DotvvmAcademy.Steps.Validation.Validators.Lesson1
{
    [StepValidation(ValidationKey = "Lesson1Step3Validator")]
    public class Lesson1Step3Validator : IDotHtmlCodeValidationObject
    {
        public void Validate(ResolvedTreeRoot resolvedTreeRoot)
        {
            ValidatorHelper.ValidateBasicControls(resolvedTreeRoot);
        }
    }
}