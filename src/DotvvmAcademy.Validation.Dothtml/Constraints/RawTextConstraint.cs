using System;
using System.Linq;
using System.Xml.XPath;
using DotVVM.Framework.Compilation.ControlTree;

namespace DotvvmAcademy.Validation.Dothtml.Constraints
{
    internal class RawTextConstraint
    {
        public RawTextConstraint(XPathExpression expression, string rawText, bool isCaseSensitive)
        {
            Expression = expression;
            RawText = rawText;
            IsCaseSensitive = isCaseSensitive;
        }

        public XPathExpression Expression { get; }

        public bool IsCaseSensitive { get; }

        public string RawText { get; }

        public void Validate(IValidationReporter reporter, NodeLocator locator)
        {
            var nodes = locator.Locate<IAbstractControl>(Expression);
            foreach (var control in nodes)
            {
                var actualContent = string.Concat(control.DothtmlNode.Tokens.Select(t => t.Text)).Trim();
                var comparison = IsCaseSensitive
                    ? StringComparison.InvariantCulture
                    : StringComparison.InvariantCultureIgnoreCase;
                if (!RawText.Equals(actualContent, comparison))
                {
                    reporter.Report(
                        message: Resources.ERR_WrongRawText,
                        arguments: new object[] { RawText },
                        node: control);
                }
            }
        }
    }
}
