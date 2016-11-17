using System.Linq;
using DotvvmAcademy.Lessons;
using DotVVM.Framework.Compilation.ControlTree.Resolved;
using DotVVM.Framework.Controls;

namespace DotvvmAcademy.Steps.Validation.Validators.Lesson1
{
    [StepValidation(ValidationKey = "Lesson1Step7Validator")]
    public class Lesson1Step7Validator : IDotHtmlCodeStepValidationObject
    {

        public void ValidateMethod(ResolvedTreeRoot resolvedTreeRoot)
        {
            resolvedTreeRoot.ValidateTextBoxBindings();

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