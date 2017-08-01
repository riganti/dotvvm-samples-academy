using DotVVM.Framework.Compilation.ControlTree.Resolved;
using DotVVM.Framework.Controls;
using DotvvmAcademy.Steps.Validation.Interfaces;
using DotvvmAcademy.Steps.Validation.ValidatorProvision;
using DotvvmAcademy.Steps.Validation.Validators.CommonValidators;
using System.Linq;

namespace DotvvmAcademy.Steps.Validation.Validators.Lesson3
{
    [StepValidation(ValidatorKey = "Lesson3Step7Validator")]
    public class Lesson3Step7Validator : IDotHtmlCodeValidationObject
    {
        public void Validate(ResolvedTreeRoot resolvedTreeRoot)
        {
            DotHtmlCommonValidator.ValidatePropertiesBindings(resolvedTreeRoot, Lesson3CommonValidator.CreateStep6Properties());

            Lesson3CommonValidator.CheckStep5Controls(resolvedTreeRoot);
            DotHtmlCommonValidator.CheckControlTypeCount<CheckBox>(resolvedTreeRoot, 3);

            var checkboxes = resolvedTreeRoot.GetDescendantControls<CheckBox>().ToList();

            if (!checkboxes.Any(c => c.GetValue(CheckableControlBase.CheckedValueProperty) == "M"))
            {
                throw new CodeValidationException(string.Format(ValidationErrorMessages.PropertyValueError, "CheckedValue", "M"));
            }
            if (!checkboxes.Any(c => c.GetValue(CheckableControlBase.CheckedValueProperty) == "S"))
            {
                throw new CodeValidationException(string.Format(ValidationErrorMessages.PropertyValueError, "CheckedValue", "S"));
            }
            if (!checkboxes.Any(c => c.GetValue(CheckableControlBase.CheckedValueProperty) == "N"))
            {
                throw new CodeValidationException(string.Format(ValidationErrorMessages.PropertyValueError, "CheckedValue", "N"));
            }
        }
    }
}