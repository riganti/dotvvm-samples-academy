using System.Collections.Generic;

namespace DotvvmAcademy.Validation.Unit
{
    public interface IValidationUnit
    {
        IEnumerable<IConstraint> GetConstraints();

        object GetAdditionalData(string key);

        void SetAdditionalData(string key, object data);
    }
}