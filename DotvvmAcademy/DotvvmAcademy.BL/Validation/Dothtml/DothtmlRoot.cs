using DotVVM.Framework.Compilation.ControlTree.Resolved;

namespace DotvvmAcademy.BL.Validation.Dothtml
{
    public class DothtmlRoot : DothtmlObject<ResolvedTreeRoot>
    {
        internal DothtmlRoot(DothtmlValidate validate, ResolvedTreeRoot node, bool isActive = true) : base(validate, node, isActive)
        {
        }

        public static DothtmlRoot Inactive { get; } = new DothtmlRoot(null, null, false);
    }
}