using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DotvvmAcademy.CourseFormat
{
    public class CodeTaskConfiguration
    {
        public CodeTaskConfiguration(string scriptPath)
        {
            ScriptPath = scriptPath;
        }

        public string ScriptPath { get; }

        public string CorrectCodePath { get; set; }

        public string DefaultCodePath { get; set; }
    }
}