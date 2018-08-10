using DotvvmAcademy.Validation.Dothtml.Unit;
using DotvvmAcademy.Validation.Dothtml.ValidationTree;
using DotvvmAcademy.Validation.Unit;
using System.Text;

namespace DotvvmAcademy.Validation.Dothtml.Unit
{
    public static class QueryExtensions_Descendants
    {
        public static IQuery<ValidationControl> GetControl(this IQuery<ValidationControl> query, string xpath)
        {
            return query.GetControls(xpath).CountEquals(1);
        }

        public static IQuery<ValidationControl> GetControl(this IQuery<ValidationPropertySetter> query, string xpath)
        {
            return query.GetControls(xpath).CountEquals(1);
        }

        public static IQuery<ValidationControl> GetControls(this IQuery<ValidationControl> query, string xpath)
        {
            return query.GetDescendantQuery<ValidationControl, ValidationControl>(xpath);
        }

        public static IQuery<ValidationControl> GetControls(this IQuery<ValidationPropertySetter> query, string xpath)
        {
            return query.GetDescendantQuery<ValidationPropertySetter, ValidationControl>(xpath);
        }

        public static IQuery<ValidationPropertySetter> GetProperties(this IQuery<ValidationControl> query, string xpath)
        {
            return query.GetDescendantQuery<ValidationControl, ValidationPropertySetter>(xpath);
        }

        public static IQuery<ValidationPropertySetter> GetProperty(this IQuery<ValidationControl> query, string xpath)
        {
            return query.GetProperties(xpath).CountEquals(1);
        }

        private static IQuery<TOut> GetDescendantQuery<TIn, TOut>(this IQuery<TIn> query, string xpath)
        {
            var dothtmlQuery = (DothtmlQuery<TIn>)query;
            var sb = new StringBuilder();
            sb.Append(query.Source);
            if (sb[sb.Length - 1] != '/')
            {
                sb.Append('/');
            }
            sb.Append(xpath);
            return dothtmlQuery.Unit.GetQuery<TOut>(sb.ToString());
        }
    }
}