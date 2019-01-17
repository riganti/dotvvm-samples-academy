using DotvvmAcademy.Validation.Dothtml.ValidationTree;
using System.Text;
using System.Xml.XPath;

namespace DotvvmAcademy.Validation.Dothtml.Unit
{
    public static class DothtmlQueryExtensions_Descendants
    {
        public static DothtmlQuery<ValidationControl> GetControl(this DothtmlQuery<ValidationControl> query, string expression)
        {
            return query.GetControls(expression).RequireCount(1);
        }

        public static DothtmlQuery<ValidationControl> GetControl(this DothtmlQuery<ValidationPropertySetter> query, string expression)
        {
            return query.GetControls(expression).RequireCount(1);
        }

        public static DothtmlQuery<ValidationControl> GetControls(this DothtmlQuery<ValidationControl> query, string expression)
        {
            return query.GetDescendantQuery<ValidationControl, ValidationControl>(expression);
        }

        public static DothtmlQuery<ValidationControl> GetControls(this DothtmlQuery<ValidationPropertySetter> query, string expression)
        {
            return query.GetDescendantQuery<ValidationPropertySetter, ValidationControl>(expression);
        }

        public static DothtmlQuery<ValidationPropertySetter> GetProperties(this DothtmlQuery<ValidationControl> query, string expression)
        {
            return query.GetDescendantQuery<ValidationControl, ValidationPropertySetter>(expression);
        }

        public static DothtmlQuery<ValidationPropertySetter> GetProperty(this DothtmlQuery<ValidationControl> query, string expression)
        {
            return query.GetProperties(expression).RequireCount(1);
        }

        private static DothtmlQuery<TOut> GetDescendantQuery<TIn, TOut>(this DothtmlQuery<TIn> query, string expression)
            where TIn : ValidationTreeNode
            where TOut : ValidationTreeNode
        {
            var sb = new StringBuilder();
            sb.Append(query.Expression.Expression);
            if (sb[sb.Length - 1] != '/')
            {
                sb.Append('/');
            }
            sb.Append(expression);
            return new DothtmlQuery<TOut>(query.Unit, XPathExpression.Compile(sb.ToString()));
        }
    }
}