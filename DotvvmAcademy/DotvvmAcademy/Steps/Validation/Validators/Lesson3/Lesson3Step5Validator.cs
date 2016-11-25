using DotvvmAcademy.Steps.Validation.Interfaces;
using DotvvmAcademy.Steps.Validation.ValidatorProvision;
using DotvvmAcademy.Steps.Validation.Validators.CommonValidators;
using DotVVM.Framework.Compilation.ControlTree.Resolved;

namespace DotvvmAcademy.Steps.Validation.Validators.Lesson3
{
    [StepValidation(ValidatorKey = "Lesson3Step5Validator")]
    public class Lesson3Step5Validator : IDotHtmlCodeValidationObject
    {
        public void Validate(ResolvedTreeRoot resolvedTreeRoot)
        {
            var propertiesToValidate = Lesson3CommonValidator.CreateStep6Properties();
            DotHtmlCommonValidator.ValidatePropertiesBindings(resolvedTreeRoot, propertiesToValidate);
            Lesson3CommonValidator.CheckStep5Controls(resolvedTreeRoot);
            DotHtmlCommonValidator.ValidatePropertiesBindings(resolvedTreeRoot,
                Lesson3CommonValidator.CreateStep4Properties());
        }
    }
}