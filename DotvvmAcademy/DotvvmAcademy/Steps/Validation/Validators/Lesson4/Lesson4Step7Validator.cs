using DotVVM.Framework.Compilation.ControlTree.Resolved;
using DotVVM.Framework.Controls;
using DotvvmAcademy.Steps.Validation.Interfaces;
using DotvvmAcademy.Steps.Validation.ValidatorProvision;
using DotvvmAcademy.Steps.Validation.Validators.CommonValidators;
using DotvvmAcademy.Steps.Validation.Validators.PropertyAndControlType;

namespace DotvvmAcademy.Steps.Validation.Validators.Lesson4
{
    [StepValidation(ValidatorKey = "Lesson4Step7Validator")]
    public class Lesson4Step7Validator : IDotHtmlCodeValidationObject
    {
        public void Validate(ResolvedTreeRoot resolvedTreeRoot)
        {
            Lesson4CommonValidator.ValidateStep5(resolvedTreeRoot);
            DotHtmlCommonValidator.CheckControlTypeCount<Validator>(resolvedTreeRoot, 1);

            DotHtmlCommonValidator.ValidatePropertyBinding(resolvedTreeRoot,
                Lesson4CommonValidator.CreateStep7ValidatorEmail());
            var property = new Property("True", "fakeProp", ControlBindName.ValidatorShowErrorMessageText);
            DotHtmlCommonValidator.ValidatePropertyBinding(resolvedTreeRoot, property);
        }
    }
}