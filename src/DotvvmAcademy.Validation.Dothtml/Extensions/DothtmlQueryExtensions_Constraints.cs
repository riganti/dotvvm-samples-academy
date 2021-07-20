using DotVVM.Framework.Compilation.ControlTree;
using DotvvmAcademy.Meta;
using DotvvmAcademy.Meta.Syntax;
using DotvvmAcademy.Validation.Dothtml.Constraints;

namespace DotvvmAcademy.Validation.Dothtml.Unit
{
    public static class DothtmlQueryExtensions_Constraints
    {
        public static DothtmlQuery<IAbstractPropertySetter> RequireBinding(
            this DothtmlQuery<IAbstractPropertySetter> query,
            string value,
            AllowedBinding kind = AllowedBinding.Value)
        {
            return query.AddConstraint(new BindingConstraint(query.Expression, value, kind));
        }

        public static DothtmlQuery<TResult> RequireCount<TResult>(this DothtmlQuery<TResult> query, int count)
            where TResult : IAbstractTreeNode
        {
            return query.AddConstraint(new CountConstraint(query.Expression, count));
        }

        public static DothtmlQuery<IAbstractControl> RequireRawContent(
            this DothtmlQuery<IAbstractControl> query,
            string expectedContent,
            bool isCaseSensitive = false)
        {
            return query.AddConstraint(new RawContentConstraint(query.Expression, expectedContent, isCaseSensitive));
        }

        public static DothtmlQuery<IAbstractControl> RequireRawText(
            this DothtmlQuery<IAbstractControl> query,
            string expected,
            bool isCaseSensitive = false)
        {
            return query.AddConstraint(new RawTextConstraint(query.Expression, expected, isCaseSensitive));
        }

        public static DothtmlQuery<IAbstractControl> RequireType<TControl>(this DothtmlQuery<IAbstractControl> query)
        {
            return query.AddConstraint(new ControlTypeConstraint(query.Expression, MetaConvert.ToMeta(typeof(TControl))));
        }

        public static DothtmlQuery<IAbstractDirective> RequireTypeArgument(
            this DothtmlQuery<IAbstractDirective> query,
            string typeFullName)
        {
            return query.AddConstraint(new TypeArgumentConstraint(query.Expression, NameNode.Parse(typeFullName)));
        }

        public static DothtmlQuery<IAbstractPropertySetter> RequireValue(
            this DothtmlQuery<IAbstractPropertySetter> query,
            object value)
        {
            return query.AddConstraint(new HardcodedValueConstraint(query.Expression, value));
        }
    }
}
