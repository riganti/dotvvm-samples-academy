using DotvvmAcademy.Steps.Validation.Interfaces;
using DotvvmAcademy.Steps.Validation.ValidatorProvision;
using DotvvmAcademy.Steps.Validation.Validators.CommonValidators;
using DotvvmAcademy.Steps.Validation.Validators.PropertyAndControlType;
using DotVVM.Framework.Compilation.ControlTree.Resolved;

namespace DotvvmAcademy.Steps.Validation.Validators.Lesson4
{
    [StepValidation(ValidatorKey = "Lesson4Step4Validator")]
    public class Lesson4Step4Validator : IDotHtmlCodeValidationObject
    {
        public void Validate(ResolvedTreeRoot resolvedTreeRoot)
        {
            Lesson4CommonValidator.ValidateStep3Properties(resolvedTreeRoot);

            var property = new Property("has-error","none",ControlBindName.DivValidatorInvalidCssClass);
            DotHtmlCommonValidator.ValidatePropertyBinding(resolvedTreeRoot,property);
        }
    }
}