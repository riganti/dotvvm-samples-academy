using DotvvmAcademy.Steps.Validation.Interfaces;
using DotvvmAcademy.Steps.Validation.ValidatorProvision;
using DotvvmAcademy.Steps.Validation.Validators.CommonValidators;
using DotVVM.Framework.Compilation.ControlTree.Resolved;
using DotVVM.Framework.Controls;

namespace DotvvmAcademy.Steps.Validation.Validators.Lesson3
{
    [StepValidation(ValidatorKey = "Lesson3Step7Validator")]
    public class Lesson3Step7Validator : IDotHtmlCodeValidationObject
    {
        public void Validate(ResolvedTreeRoot resolvedTreeRoot)
        {

            DotHtmlCommonValidator.ValidatePropertiesBindings(resolvedTreeRoot,
               Lesson3CommonValidator.CreateStep6Properties());
            ValidationExtensions.CheckTypeAndCount<RadioButton>(resolvedTreeRoot, 2);
            ValidationExtensions.CheckTypeAndCount<TextBox>(resolvedTreeRoot, 2);
            ValidationExtensions.CheckTypeAndCount<CheckBox>(resolvedTreeRoot, 3);

        }
    }
}