using System;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Threading.Tasks;

namespace DotvvmAcademy.CourseFormat
{
    internal class CodeTask : ICodeTask
    {
        private readonly ValidationService validationService;

        public CodeTask(CodeTaskId id, ValidationService validationService)
        {
            Id = id;
            this.validationService = validationService;
        }

        public string Code { get; set; }

        public CodeTaskId Id { get; }

        public string Language { get; set; }

        public Task<ImmutableArray<ICodeTaskDiagnostic>> Validate(string userCode)
        {
            return validationService.Validate(this, userCode);
        }
    }
}