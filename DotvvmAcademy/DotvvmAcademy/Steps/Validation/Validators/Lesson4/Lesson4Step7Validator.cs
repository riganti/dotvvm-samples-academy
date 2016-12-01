using System.Linq;
using DotvvmAcademy.Steps.Validation.Interfaces;
using DotvvmAcademy.Steps.Validation.ValidatorProvision;
using DotvvmAcademy.Steps.Validation.Validators.CommonValidators;
using DotVVM.Framework.Compilation.ControlTree.Resolved;
using DotVVM.Framework.Controls;

namespace DotvvmAcademy.Steps.Validation.Validators.Lesson4
{
    [StepValidation(ValidatorKey = "Lesson4Step7Validator")]
    public class Lesson4Step7Validator : IDotHtmlCodeValidationObject
    {
        public void Validate(ResolvedTreeRoot resolvedTreeRoot)
        {
            Lesson4CommonValidator.ValidateStep5(resolvedTreeRoot);
            DotHtmlCommonValidator.CheckControlTypeCount<Validator>(resolvedTreeRoot,1);


            var h =
                resolvedTreeRoot.GetDescendantControls<Validator>()
                    .FirstOrDefault()
                    .Properties.FirstOrDefault(a => a.Key == Validator.ShowErrorMessageTextProperty)
                    .Value;
        }
    }
}