using DotvvmAcademy.Validation.Dothtml.ValidationTree;
using System;
using System.Collections.Generic;

namespace DotvvmAcademy.Validation.Dothtml.Unit
{
    public class DothtmlQuery
    {
        private List<Action<ConstraintContext>> registered = new List<Action<ConstraintContext>>();

        public DothtmlQuery(string xpath)
        {
            XPath = xpath;
        }

        public string XPath { get; }

        public void AddConstraint(Action<ConstraintContext> constraint) => registered.Add(constraint);
    }

    public class DothtmlQuery<TResult> : DothtmlQuery
        where TResult : ValidationTreeNode
    {
        public DothtmlQuery(string xpath) : base(xpath)
        {
        }

        public void AddConstraint(Action<ConstraintContext<TResult>> constraint) => AddConstraint(constraint);
    }
}