using DotvvmAcademy.Steps.Validation.Interfaces;
using DotvvmAcademy.Steps.Validation.ValidatorProvision;
using DotVVM.Framework.Compilation.ControlTree.Resolved;

namespace DotvvmAcademy.Steps.Validation.Validators.Lesson3
{
    [StepValidation(ValidatorKey = "Lesson3Step3Validator")]
    public class Lesson3Step3Validator : IDotHtmlCodeValidationObject
    {
        public void Validate(ResolvedTreeRoot resolvedTreeRoot)
        {
            Lesson3CommonValidator.Step3Validator(resolvedTreeRoot);
        }
    }
}