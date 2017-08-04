using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotvvmAcademy.CommonMark
{
    public class ControlParserException : Exception
    {
        public ControlParserException(string message) : base(message)
        {
        }
    }
}
