using DotvvmAcademy.Validation.Dothtml.ValidationTree;
using DotvvmAcademy.Validation.Unit;
using System;
using System.Collections.Generic;

namespace DotvvmAcademy.Validation.Dothtml.Unit
{
    public class DothtmlQuery<TResult> : IQuery<TResult>
        where TResult : ValidationTreeNode
    {
        private readonly List<Action<DothtmlConstraintContext<TResult>>> registered
            = new List<Action<DothtmlConstraintContext<TResult>>>();

        public DothtmlQuery(string xpath)
        {
            XPath = xpath;
        }

        public string XPath { get; }

        public void AddConstraint(Action<DothtmlConstraintContext<TResult>> constraint) => AddConstraint(constraint);

        void IQuery<TResult>.AddConstraint(Action<IConstraintContext<TResult>> constraint) => AddConstraint(constraint);

        void IQuery.AddConstraint(Action<IConstraintContext> constraint) => AddConstraint(constraint);
    }
}