using System;
using System.Collections.Generic;
using System.Text;

namespace DotvvmAcademy.CourseFormat
{
    internal class CodeTaskDiagnostic : ICodeTaskDiagnostic
    {
        public int End { get; set; }

        public CodeTaskDiagnosticSeverity Severity { get; set; }

        public int Start { get; set; }
    }
}
