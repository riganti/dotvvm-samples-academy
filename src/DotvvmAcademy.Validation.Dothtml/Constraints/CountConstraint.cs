using DotvvmAcademy.Validation.Dothtml.ValidationTree;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.XPath;

namespace DotvvmAcademy.Validation.Dothtml.Constraints
{
    internal class CountConstraint
    {
        public CountConstraint(XPathExpression expression, int count)
        {
            Expression = expression;
            Count = count;
        }

        public int Count { get; }

        public XPathExpression Expression { get; }

        public void Validate(IValidationReporter reporter, NodeLocator locator)
        {
            var nodes = locator.Locate(Expression)
                .ToArray();

            // Correct count
            if (nodes.Length == Count)
            {
                return;
            }

            if (nodes.Length > 0)
            {
                var kind = XPathUtilities.GetKind(Expression.Expression);
                var name = XPathUtilities.GetName(Expression.Expression);
                if(XPathUtilities.IsTopLevel(Expression.Expression))
                {
                    var message = Resources.ResourceManager.GetString($"ERR_Wrong{kind}CountRoot");
                    reporter.Report(message, new[] { name });
                }
                else
                {
                    var parentPath = Expression.GetLogicalParent();
                    var parent = locator.LocateSingle<ValidationControl>(parentPath);
                    var message = Resources.ResourceManager.GetString($"ERR_Wrong{kind}Count");
                    reporter.Report(message, new[] { name }, parent);
                }
            }
            else
            {
                var currentPath = Expression;
                while(!XPathUtilities.IsTopLevel(currentPath.Expression))
                {
                    var parentPath = currentPath.GetLogicalParent();
                    var parents = locator.Locate(parentPath).ToArray();
                    if (parents.Length == 0)
                    {
                        currentPath = parentPath;
                    }
                    else
                    {
                        var kind = XPathUtilities.GetKind(currentPath.Expression);
                        var name = XPathUtilities.GetName(currentPath.Expression);
                        var message = Resources.ResourceManager.GetString($"ERR_Missing{kind}");
                        foreach(var parent in parents)
                        {
                            reporter.Report(message, new[] { name }, parent);
                        }
                        return;
                    }
                }
                {
                    var kind = XPathUtilities.GetKind(currentPath.Expression);
                    var name = XPathUtilities.GetName(currentPath.Expression);
                    var message = Resources.ResourceManager.GetString($"ERR_Missing{kind}Root");
                    reporter.Report(message, new[] { name });
                }
            }
        }
    }
}