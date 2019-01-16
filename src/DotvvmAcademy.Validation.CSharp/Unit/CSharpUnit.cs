using DotvvmAcademy.Validation.Unit;
using System;
using System.Collections.Generic;
using System.Text;

namespace DotvvmAcademy.Validation.CSharp.Unit
{
    public class CSharpUnit : IValidationUnit
    {
        internal Dictionary<string, IConstraint> Constraints { get; } = new Dictionary<string, IConstraint>();

        public IEnumerable<IConstraint> GetConstraints()
        {
            return Constraints.Values;
        }

        internal void AddOverwritableConstraint<TConstraint>(TConstraint constraint, params object[] parameters)
        {
            var sb = new StringBuilder();
            sb.Append(typeof(TConstraint).Name);
            foreach (var parameter in parameters)
            {
                sb.Append('_');
                sb.Append(parameter);
            }
            AddConstraint(sb.ToString(), constraint);
        }

        internal void AddUniqueConstraint<TConstraint>(TConstraint constraint)
        {
            var key = $"{typeof(TConstraint).Name}_{Guid.NewGuid()}";
            AddConstraint(key, constraint);
        }

        private void AddConstraint<TConstraint>(string key, TConstraint constraint)
        {
            if (constraint is IConstraint interfaceConstraint)
            {
                Constraints.Add(key, interfaceConstraint);
            }
            else
            {
                Constraints.Add(key, new ConventionConstraint<TConstraint>(constraint));
            }
        }
    }
}