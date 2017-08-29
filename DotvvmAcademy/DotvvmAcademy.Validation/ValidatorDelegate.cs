using System.Collections.Generic;

namespace DotvvmAcademy.Validation
{
    public delegate IEnumerable<ValidationError> ValidatorDelegate(string code, IEnumerable<string> dependencies = null);
}