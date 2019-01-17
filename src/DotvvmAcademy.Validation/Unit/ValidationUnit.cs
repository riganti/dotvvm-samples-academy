using System.Collections.Generic;
using System.Text;

namespace DotvvmAcademy.Validation.Unit
{
    public abstract class ValidationUnit
    {
        public Dictionary<string, IConstraint> Constraints { get; } = new Dictionary<string, IConstraint>();

        public void AddConstraint<TConstraint>(TConstraint constraint, params object[] parameters)
        {
            var sb = new StringBuilder();
            sb.Append(typeof(TConstraint).Name);
            foreach (var parameter in parameters)
            {
                sb.Append('_');
                sb.Append(parameter);
            }
            var key = sb.ToString();
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