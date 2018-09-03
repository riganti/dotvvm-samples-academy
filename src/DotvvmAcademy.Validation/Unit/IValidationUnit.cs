using System;
using System.Collections.Generic;

namespace DotvvmAcademy.Validation.Unit
{
    public interface IValidationUnit
    {
        IList<IConstraint> Constraints { get; }

        IServiceProvider Provider { get; }
    }
}