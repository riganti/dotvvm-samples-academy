using DotvvmAcademy.Validation.Dothtml.ValidationTree;
using System;
using System.Linq;
using System.Xml.XPath;

namespace DotvvmAcademy.Validation.Dothtml.Constraints
{
    internal class RawContentConstraint
    {
        public RawContentConstraint(XPathExpression expression, string rawContent, bool isCaseSensitive)
        {
            Expression = expression;
            RawContent = rawContent;
            IsCaseSensitive = isCaseSensitive;
        }

        public XPathExpression Expression { get; }

        public bool IsCaseSensitive { get; }

        public string RawContent { get; }

        public void Validate(IValidationReporter reporter, NodeLocator locator)
        {
            var nodes = locator.Locate<ValidationControl>(Expression);
            foreach (var control in nodes)
            {
                var innerTokens = control.Content.SelectMany(c => c.DothtmlNode.Tokens);
                var actualContent = string.Concat(innerTokens.Select(t => t.Text))
                    .Trim();
                var comparison = IsCaseSensitive
                    ? StringComparison.InvariantCulture
                    : StringComparison.InvariantCultureIgnoreCase;
                if (!RawContent.Equals(actualContent, comparison))
                {
                    reporter.Report(
                        message: Resources.ERR_WrongRawContent,
                        arguments: new object[] { RawContent },
                        node: control);
                }
            }
        }
    }
}