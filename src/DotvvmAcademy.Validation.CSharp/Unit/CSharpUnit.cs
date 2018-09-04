using DotvvmAcademy.Validation.Unit;
using System;
using System.Collections.Generic;

namespace DotvvmAcademy.Validation.CSharp.Unit
{
    public class CSharpUnit : IValidationUnit
    {
        public CSharpUnit(IServiceProvider provider)
        {
            Provider = provider;
        }

        public ICollection<IConstraint> Constraints { get; } = new HashSet<IConstraint>(new ConstraintEqualityComparer());

        public ICollection<Action<CSharpDynamicContext>> DynamicActions { get; } = new List<Action<CSharpDynamicContext>>();

        public IServiceProvider Provider { get; }
    }
}