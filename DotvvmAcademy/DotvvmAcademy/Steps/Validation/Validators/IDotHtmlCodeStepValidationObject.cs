using DotvvmAcademy.Steps.Validation.Interfaces;
using DotVVM.Framework.Compilation.ControlTree.Resolved;

namespace DotvvmAcademy.Steps.Validation.Validators
{
    public interface IDotHtmlCodeStepValidationObject : ILessonValidationObject
    {
        void ValidateMethod(ResolvedTreeRoot resolvedTreeRoot);
    }
}