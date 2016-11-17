using DotVVM.Framework.Compilation.ControlTree.Resolved;

namespace DotvvmAcademy.Steps.Validation.Validators.Lesson1
{
    [StepValidation(ValidationKey = "Lesson1Step3Validator")]
    public class Lesson1Step3Validator : IDotHtmlCodeStepValidationObject
    {
        public void ValidateMethod(ResolvedTreeRoot resolvedTreeRoot)
        {
            resolvedTreeRoot.ValidateBasicControls();
        }
    }
}