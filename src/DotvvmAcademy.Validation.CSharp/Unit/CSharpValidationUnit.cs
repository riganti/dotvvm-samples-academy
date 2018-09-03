using DotvvmAcademy.Validation.Unit;
using System;
using System.Collections.Generic;

namespace DotvvmAcademy.Validation.CSharp.Unit
{
    public class CSharpValidationUnit : IValidationUnit
    {
        public CSharpValidationUnit(IServiceProvider provider)
        {
            Provider = provider;
        }

        public IList<IConstraint> Constraints { get; } = new List<IConstraint>();

        public IServiceProvider Provider { get; }
    }
}