using DotvvmAcademy.Validation.Dothtml.Unit;
using DotvvmAcademy.Validation.Dothtml.ValidationTree;
using DotvvmAcademy.Validation.Unit;
using System.Text;
using System.Xml.XPath;

namespace DotvvmAcademy.Validation.Dothtml.Unit
{
    public static class DothtmlQueryExtensions_Descendants
    {
        public static DothtmlQuery<ValidationControl> GetControl(this DothtmlQuery<ValidationControl> query, string xpath)
        {
            return query.GetControls(xpath).CountEquals(1);
        }

        public static DothtmlQuery<ValidationControl> GetControl(this DothtmlQuery<ValidationPropertySetter> query, string xpath)
        {
            return query.GetControls(xpath).CountEquals(1);
        }

        public static DothtmlQuery<ValidationControl> GetControls(this DothtmlQuery<ValidationControl> query, string xpath)
        {
            return query.GetDescendantQuery<ValidationControl, ValidationControl>(xpath);
        }

        public static DothtmlQuery<ValidationControl> GetControls(this DothtmlQuery<ValidationPropertySetter> query, string xpath)
        {
            return query.GetDescendantQuery<ValidationPropertySetter, ValidationControl>(xpath);
        }

        public static DothtmlQuery<ValidationPropertySetter> GetProperties(this DothtmlQuery<ValidationControl> query, string xpath)
        {
            return query.GetDescendantQuery<ValidationControl, ValidationPropertySetter>(xpath);
        }

        public static DothtmlQuery<ValidationPropertySetter> GetProperty(this DothtmlQuery<ValidationControl> query, string xpath)
        {
            return query.GetProperties(xpath).CountEquals(1);
        }

        private static DothtmlQuery<TOut> GetDescendantQuery<TIn, TOut>(this DothtmlQuery<TIn> query, string xpath)
            where TIn : ValidationTreeNode 
            where TOut : ValidationTreeNode
        {
            var sb = new StringBuilder();
            sb.Append(query.XPath.Expression);
            if (sb[sb.Length - 1] != '/')
            {
                sb.Append('/');
            }
            sb.Append(xpath);
            return new DothtmlQuery<TOut>(query.Unit, XPathExpression.Compile(sb.ToString()));
        }
    }
}