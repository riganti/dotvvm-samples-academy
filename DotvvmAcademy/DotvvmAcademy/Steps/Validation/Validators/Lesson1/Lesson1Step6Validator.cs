using DotvvmAcademy.Steps.Validation.Interfaces;
using DotvvmAcademy.Steps.Validation.ValidatorProvision;
using DotvvmAcademy.Steps.Validation.Validators.CommonValidators;
using DotVVM.Framework.Compilation.ControlTree.Resolved;

namespace DotvvmAcademy.Steps.Validation.Validators.Lesson1
{
    [StepValidation(ValidatorKey = "Lesson1Step6Validator")]
    public class Lesson1Step6Validator : IDotHtmlCodeValidationObject
    {
        public void Validate(ResolvedTreeRoot resolvedTreeRoot)
        {
            DotHtmlCommonValidator.ValidateTextBoxBindings(resolvedTreeRoot);
        }
    }
}