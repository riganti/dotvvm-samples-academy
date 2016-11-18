using DotVVM.Framework.Compilation.ControlTree.Resolved;

namespace DotvvmAcademy.Steps.Validation.Interfaces
{
    public interface IDotHtmlCodeValidationObject : ILessonValidationObject
    {
        void Validate(ResolvedTreeRoot resolvedTreeRoot);
    }
}