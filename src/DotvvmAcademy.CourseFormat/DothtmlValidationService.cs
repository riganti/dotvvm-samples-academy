using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;
using System.Threading.Tasks;

namespace DotvvmAcademy.CourseFormat
{
    internal class DothtmlValidationService : IValidationService
    {
        public Task<ImmutableArray<ICodeTaskDiagnostic>> Validate(ICodeTask task, string code)
        {
            throw new NotImplementedException();
        }
    }
}
