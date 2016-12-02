using DotvvmAcademy.Steps.Validation.Interfaces;
using DotvvmAcademy.Steps.Validation.ValidatorProvision;
using DotvvmAcademy.Steps.Validation.Validators.CommonValidators;
using DotVVM.Framework.Compilation.ControlTree.Resolved;

namespace DotvvmAcademy.Steps.Validation.Validators.Lesson4
{
    [StepValidation(ValidatorKey = "Lesson4Step3Validator")]
    public class Lesson4Step3Validator : IDotHtmlCodeValidationObject
    {
        public void Validate(ResolvedTreeRoot resolvedTreeRoot)
        {
            DotHtmlCommonValidator.CheckCountOfHtmlTag(resolvedTreeRoot, "div", 3);
            Lesson4CommonValidator.ValidateStep2ValidationProperties(resolvedTreeRoot);
            Lesson4CommonValidator.ValidateOnlyStep3Properties(resolvedTreeRoot);
        }
    }
}