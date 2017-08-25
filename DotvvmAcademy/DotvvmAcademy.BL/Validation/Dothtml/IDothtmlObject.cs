using DotVVM.Framework.Compilation.ControlTree.Resolved;

namespace DotvvmAcademy.BL.Validation.Dothtml
{
    public interface IDothtmlObject<out TNode> : IValidationObject<DothtmlValidate>
        where TNode : ResolvedContentNode
    {
        TNode Node { get; }

        DothtmlControlCollection Children();

        DothtmlControlCollection Children(int count);
    }
}