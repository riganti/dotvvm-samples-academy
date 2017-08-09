using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotvvmAcademy.BL.CommonMark
{
    public class ComponentParserException : Exception
    {
        public ComponentParserException(string message) : base(message)
        {
        }
    }
}
