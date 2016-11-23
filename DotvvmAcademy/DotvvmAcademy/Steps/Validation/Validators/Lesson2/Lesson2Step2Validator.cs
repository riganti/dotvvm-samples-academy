using DotvvmAcademy.Steps.Validation.Interfaces;
using DotvvmAcademy.Steps.Validation.ValidatorProvision;
using DotvvmAcademy.Steps.Validation.Validators.CommonValidators;
using DotVVM.Framework.Compilation.ControlTree.Resolved;

namespace DotvvmAcademy.Steps.Validation.Validators.Lesson2
{
    [StepValidation(ValidatorKey = "Lesson2Step2Validator")]
    public class Lesson2Step2Validator : IDotHtmlCodeValidationObject
    {
        public void Validate(ResolvedTreeRoot resolvedTreeRoot)
        {
            DotHtmlCommonValidator.ValidateAddTaskControls(resolvedTreeRoot);
        }
    }
}