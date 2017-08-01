using DotVVM.Framework.Compilation.ControlTree.Resolved;
using DotvvmAcademy.Steps.Validation.Interfaces;
using DotvvmAcademy.Steps.Validation.ValidatorProvision;
using DotvvmAcademy.Steps.Validation.Validators.CommonValidators;

namespace DotvvmAcademy.Steps.Validation.Validators.Lesson3
{
    [StepValidation(ValidatorKey = "Lesson3Step3Validator")]
    public class Lesson3Step3Validator : IDotHtmlCodeValidationObject
    {
        public void Validate(ResolvedTreeRoot resolvedTreeRoot)
        {
            Lesson3CommonValidator.CheckStep3Controls(resolvedTreeRoot);
            DotHtmlCommonValidator.ValidatePropertiesBindings(resolvedTreeRoot, Lesson3CommonValidator.CreateStep2Properties());
        }
    }
}