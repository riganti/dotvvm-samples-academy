using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotvvmAcademy.CourseFormat
{
    public class ValidationScript
    {
        public ValidationScript(string entryType, string entryMethod, byte[] bytes)
        {
            EntryType = entryType;
            EntryMethod = entryMethod;
            Bytes = bytes;
        }

        public string EntryType { get; }

        public string EntryMethod { get; }

        public byte[] Bytes { get; }
    }
}
