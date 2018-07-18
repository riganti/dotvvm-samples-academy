using System;
using System.Diagnostics;
using System.IO;

namespace DotvvmAcademy.CourseFormat
{
    [DebuggerDisplay("CodeTaskId: {CodePath}, {ValidationScriptPath}")]
    public sealed class CodeTaskId : IResourceId
    {
        internal CodeTaskId(StepId stepId, string codeFile, string validationScriptFile)
        {
            StepId = stepId;
            Path = stepId.Path;
            CodePath = $"{stepId.Path}/{codeFile}";
            ValidationScriptPath = $"{stepId.Path}/{validationScriptFile}";
            Language = System.IO.Path.GetExtension(codeFile);
        }

        public string Path { get; }

        public string CodePath { get; }

        public string Language { get; }

        public StepId StepId { get; }

        public string ValidationScriptPath { get; }
    }
}