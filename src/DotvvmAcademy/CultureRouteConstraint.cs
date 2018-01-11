using DotVVM.Framework.Routing;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace DotvvmAcademy
{
    public class CultureRouteConstraint : IRouteParameterConstraint
    {
        public CultureRouteConstraint(IEnumerable<CultureInfo> supportedCultures)
        {
            SupportedCultures = supportedCultures.ToList();
        }

        public List<CultureInfo> SupportedCultures { get; }

        public string GetPartRegex(string parameter)
        {
            var sb = new StringBuilder();
            sb.Append('(');
            for (int i = 0; i < SupportedCultures.Count; i++)
            {
                sb.Append(SupportedCultures[i].ToString());
                if (i != SupportedCultures.Count - 1)
                {
                    sb.Append('|');
                }
            }
            sb.Append(')');
            return sb.ToString();
        }

        public ParameterParseResult ParseString(string value, string parameter)
        {
            return ParameterParseResult.Create(new CultureInfo(value));
        }
    }
}