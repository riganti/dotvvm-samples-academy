using System.Linq;
using DotvvmAcademy.Lessons;
using DotVVM.Framework.Compilation.ControlTree.Resolved;
using DotVVM.Framework.Controls;

namespace DotvvmAcademy.Steps.Validation.Validators.Lesson1
{
    [StepValidation(ValidationKey = "Lesson1Step6Validator")]
    public class Lesson1Step6Validator : IDotHtmlCodeStepValidationObject
    {
      
        public void ValidateMethod(ResolvedTreeRoot resolvedTreeRoot)
        {
            resolvedTreeRoot.ValidateTextBoxBindings();
        }
    }
}