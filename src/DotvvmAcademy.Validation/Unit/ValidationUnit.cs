using System.Collections.Generic;
using System.Text;

namespace DotvvmAcademy.Validation.Unit
{
    public abstract class ValidationUnit : IValidationUnit
    {
        public Dictionary<string, object> AdditionalData { get; } = new Dictionary<string, object>();

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

        public object GetAdditionalData(string key)
        {
            if (AdditionalData.TryGetValue(key, out var value))
            {
                return value;
            }
            return null;
        }

        public IEnumerable<IConstraint> GetConstraints()
        {
            return Constraints.Values;
        }

        public void SetAdditionalData(string key, object value)
        {
            AdditionalData[key] = value;
        }
    }
}