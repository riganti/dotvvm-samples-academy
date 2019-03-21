using DotvvmAcademy.Meta;
using DotvvmAcademy.Meta.Syntax;
using DotvvmAcademy.Validation.Dothtml.Constraints;
using DotvvmAcademy.Validation.Dothtml.ValidationTree;

namespace DotvvmAcademy.Validation.Dothtml.Unit
{
    public static class DothtmlQueryExtensions_Constraints
    {
        public static DothtmlQuery<ValidationPropertySetter> RequireBinding(
            this DothtmlQuery<ValidationPropertySetter> query,
            string value,
            AllowedBinding kind = AllowedBinding.Value)
        {
            return query.AddConstraint(new BindingConstraint(query.Expression, value, kind));
        }

        public static DothtmlQuery<TResult> RequireCount<TResult>(this DothtmlQuery<TResult> query, int count)
            where TResult : ValidationTreeNode
        {
            return query.AddConstraint(new CountConstraint(query.Expression, count));
        }

        public static DothtmlQuery<ValidationControl> RequireRawContent(
            this DothtmlQuery<ValidationControl> query,
            string expectedContent,
            bool isCaseSensitive = false)
        {
            return query.AddConstraint(new RawContentConstraint(query.Expression, expectedContent, isCaseSensitive));
        }

        public static DothtmlQuery<ValidationControl> RequireRawText(
            this DothtmlQuery<ValidationControl> query,
            string expected,
            bool isCaseSensitive = false)
        {
            return query.AddConstraint(new RawTextConstraint(query.Expression, expected, isCaseSensitive));
        }

        public static DothtmlQuery<ValidationControl> RequireType<TControl>(this DothtmlQuery<ValidationControl> query)
        {
            return query.AddConstraint(new ControlTypeConstraint(query.Expression, MetaConvert.ToMeta(typeof(TControl))));
        }

        public static DothtmlQuery<ValidationDirective> RequireTypeArgument(
            this DothtmlQuery<ValidationDirective> query,
            string typeFullName)
        {
            return query.AddConstraint(new TypeArgumentConstraint(query.Expression, NameNode.Parse(typeFullName)));
        }

        public static DothtmlQuery<ValidationPropertySetter> RequireValue(
            this DothtmlQuery<ValidationPropertySetter> query,
            object value)
        {
            return query.AddConstraint(new HardcodedValueConstraint(query.Expression, value));
        }
    }
}