using System.Linq;
using DotvvmAcademy.Steps.Validation.Interfaces;
using DotvvmAcademy.Steps.Validation.ValidatorProvision;
using DotvvmAcademy.Steps.Validation.Validators.CommonValidators;
using DotVVM.Framework.Compilation.ControlTree.Resolved;
using DotVVM.Framework.Controls;

namespace DotvvmAcademy.Steps.Validation.Validators.Lesson1
{
    [StepValidation(ValidatorKey = "Lesson1Step7Validator")]
    public class Lesson1Step7Validator : IDotHtmlCodeValidationObject
    {
        public void Validate(ResolvedTreeRoot resolvedTreeRoot)
        {
            DotHtmlCommonValidator.ValidateTextBoxBindings(resolvedTreeRoot);

            resolvedTreeRoot.GetDescendantControls<Button>().Single()
                .ValidateCommandBindingExpression(ButtonBase.ClickProperty, "Calculate()");

            var buttonTextBinding = resolvedTreeRoot.GetDescendantControls<Button>()
                .Select(c => c.GetValue(ButtonBase.TextProperty))
                .SingleOrDefault();

            if (buttonTextBinding == null)
            {
                throw new CodeValidationException(Lesson1Texts.ButtonDoesNotHaveText);
            }
        }
    }
}