using Microsoft.Extensions.DependencyInjection;
using System;
using System.Xml.XPath;

namespace DotvvmAcademy.Validation.Dothtml.Unit
{
    public class DothtmlUnit : Validation.Unit.Unit
    {
        public DothtmlUnit(IServiceProvider provider) : base(provider)
        {
        }

        public DothtmlQuery<TResult> GetQuery<TResult>(string xpath)
        {
            var expression = XPathExpression.Compile(xpath);
            var query = ActivatorUtilities.CreateInstance<DothtmlQuery<TResult>>(Provider, expression);
            AddQuery(query);
            return query;
        }
    }
}