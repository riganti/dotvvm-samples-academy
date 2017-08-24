using System;
using System.Collections.Generic;
using System.Text;

namespace DotvvmAcademy.BL.Validation
{
    public delegate IEnumerable<ValidationError> ValidatorDelegate(string code, IEnumerable<string> dependencies = null);
}
