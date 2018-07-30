using DotvvmAcademy.Validation.Dothtml.ValidationTree;
using DotvvmAcademy.Validation.Unit;
using System;
using System.Collections.Generic;

namespace DotvvmAcademy.Validation.Dothtml.Unit
{
    public class DothtmlQuery<TResult> : IQuery<TResult>
        where TResult : ValidationTreeNode
    {
        public DothtmlQuery(string xpath)
        {
            XPath = xpath;
        }

        public List<Action<DothtmlConstraintContext<TResult>>> Constraints { get; }
            = new List<Action<DothtmlConstraintContext<TResult>>>();

        public string XPath { get; }

        public void AddConstraint(Action<DothtmlConstraintContext<TResult>> constraint) => Constraints.Add(constraint);

        void IQuery<TResult>.AddConstraint(Action<IConstraintContext<TResult>> constraint) => AddConstraint(constraint);

        void IQuery.AddConstraint(Action<IConstraintContext> constraint) => AddConstraint(constraint);
    }
}