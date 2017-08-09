using System.Collections.Generic;

namespace DotvvmAcademy.BL.Validation
{
    public delegate IEnumerable<string> ValidationDelegate(string correctCode, string incorrectCode);
}