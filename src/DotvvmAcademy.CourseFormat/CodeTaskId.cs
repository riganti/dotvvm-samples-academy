using System;
using System.Diagnostics;
using System.IO;

namespace DotvvmAcademy.CourseFormat
{
    [DebuggerDisplay("CodeTaskId: {CodePath}, {ValidationScriptPath}")]
    public sealed class CodeTaskId
    {
        internal CodeTaskId(StepId stepId, string codeFile, string validationScriptFile)
        {
            StepId = stepId;
            CodePath = $"{stepId.Path}/{codeFile}";
            ValidationScriptPath = $"{stepId.Path}/{validationScriptFile}";
            Language = Path.GetExtension(codeFile);
            Id = Guid.NewGuid();
        }

        public string CodePath { get; }

        public Guid Id { get; }

        public string Language { get; }

        public StepId StepId { get; }

        public string ValidationScriptPath { get; }
    }
}