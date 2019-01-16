using System.Collections.Generic;

namespace DotvvmAcademy.Validation.Unit
{
    public interface IValidationUnit
    {
        IEnumerable<IConstraint> GetConstraints();
    }
}