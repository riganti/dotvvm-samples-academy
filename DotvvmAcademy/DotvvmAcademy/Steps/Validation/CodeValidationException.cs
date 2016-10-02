using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotvvmAcademy.Steps.Validation
{
    public class CodeValidationException : Exception 
    {

        public CodeValidationException(string message) : base(message)
        {
        }

        public CodeValidationException(string message, Exception innerException) : base(message, innerException)
        {
        }

    }
}
