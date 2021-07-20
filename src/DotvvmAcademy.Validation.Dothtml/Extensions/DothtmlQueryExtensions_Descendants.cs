using System.Text;
using System.Xml.XPath;
using DotVVM.Framework.Compilation.ControlTree;

namespace DotvvmAcademy.Validation.Dothtml.Unit
{
    public static class DothtmlQueryExtensions_Descendants
    {
        public static DothtmlQuery<IAbstractControl> GetControl(this DothtmlQuery<IAbstractControl> query, string expression)
        {
            return query.GetControls(expression).RequireCount(1);
        }

        public static DothtmlQuery<IAbstractControl> GetControl(this DothtmlQuery<IAbstractPropertySetter> query, string expression)
        {
            return query.GetControls(expression).RequireCount(1);
        }

        public static DothtmlQuery<IAbstractControl> GetControls(this DothtmlQuery<IAbstractControl> query, string expression)
        {
            return query.GetDescendantQuery<IAbstractControl, IAbstractControl>(expression);
        }

        public static DothtmlQuery<IAbstractControl> GetControls(this DothtmlQuery<IAbstractPropertySetter> query, string expression)
        {
            return query.GetDescendantQuery<IAbstractPropertySetter, IAbstractControl>(expression);
        }

        public static DothtmlQuery<IAbstractPropertySetter> GetProperties(this DothtmlQuery<IAbstractControl> query, string expression)
        {
            return query.GetDescendantQuery<IAbstractControl, IAbstractPropertySetter>(expression);
        }

        public static DothtmlQuery<IAbstractPropertySetter> GetProperty(this DothtmlQuery<IAbstractControl> query, string expression)
        {
            return query.GetProperties(expression).RequireCount(1);
        }

        private static DothtmlQuery<TOut> GetDescendantQuery<TIn, TOut>(this DothtmlQuery<TIn> query, string expression)
            where TIn : IAbstractTreeNode
            where TOut : IAbstractTreeNode
        {
            var sb = new StringBuilder();
            sb.Append(query.Expression.Expression);
            if (sb[^1] != '/')
            {
                sb.Append('/');
            }
            sb.Append(expression);
            return new DothtmlQuery<TOut>(query.Unit, XPathExpression.Compile(sb.ToString()));
        }
    }
}
