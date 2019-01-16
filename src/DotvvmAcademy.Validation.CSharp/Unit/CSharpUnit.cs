using DotvvmAcademy.Validation.Unit;
using System;
using System.Collections.Generic;

namespace DotvvmAcademy.Validation.CSharp.Unit
{
    public class CSharpUnit : IValidationUnit
    {
        internal Dictionary<string, IConstraint> Constraints = new Dictionary<string, IConstraint>();

        internal void AddUniqueConstraint<TConstraint>(TConstraint constraint)
        {
            var key = $"{typeof(TConstraint).Name}_{Guid.NewGuid()}";
            if (constraint is IConstraint interfaceConstraint)
            {
                Constraints.Add(key, interfaceConstraint);
            }
            else
            {
                Constraints.Add(key, new ConventionConstraint<TConstraint>(constraint));
            }
        }

        public IEnumerable<IConstraint> GetConstraints()
        {
            return Constraints.Values;
        }
    }
}