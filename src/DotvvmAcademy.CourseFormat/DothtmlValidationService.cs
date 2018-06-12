using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;
using System.Threading.Tasks;

namespace DotvvmAcademy.CourseFormat
{
    internal class DothtmlValidationService : IValidationService
    {
        public Task<ImmutableArray<ICodeTaskDiagnostic>> Validate(CourseWorkspace workspace, CodeTaskId id, string code)
        {
            throw new NotImplementedException();
        }
    }
}
