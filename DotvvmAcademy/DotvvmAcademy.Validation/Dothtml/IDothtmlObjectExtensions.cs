using DotVVM.Framework.Compilation.ControlTree.Resolved;
using DotVVM.Framework.Controls;

namespace DotvvmAcademy.Validation.Dothtml
{
    public static class IDothtmlObjectExtensions
    {
        public static DothtmlControl Control<TControl>(this IDothtmlObject<ResolvedContentNode> o)
            where TControl : DotvvmControl => o.Children().Control<TControl>();

        public static DothtmlControlCollection Controls<TControl>(this IDothtmlObject<ResolvedContentNode> o)
            where TControl : DotvvmControl => o.Children().Controls<TControl>();

        public static DothtmlControlCollection Controls<TControl>(this IDothtmlObject<ResolvedContentNode> o, int count)
            where TControl : DotvvmControl => o.Children().Controls<TControl>(count);

        public static DothtmlControl Element(this IDothtmlObject<ResolvedContentNode> o, string tagName, string tagPrefix = null)
            => o.Children().Element(tagName, tagPrefix);

        public static DothtmlControlCollection Elements(this IDothtmlObject<ResolvedContentNode> o, string tagName, string tagPrefix = null)
            => o.Children().Elements(tagName, tagPrefix);

        public static DothtmlControlCollection Elements(this IDothtmlObject<ResolvedContentNode> o, int count, string tagName, string tagPrefix = null)
            => o.Children().Elements(count, tagName, tagPrefix);

        public static DothtmlControlCollection Elements(this IDothtmlObject<ResolvedContentNode> o, int? count = null)
            => o.Children().Elements(count);
    }
}