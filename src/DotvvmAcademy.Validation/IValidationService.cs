using System.Collections.Generic;
using System.Threading.Tasks;

namespace DotvvmAcademy.Validation
{
    public interface IValidationService
    {
        Task<IEnumerable<IValidationDiagnostic>> Validate(IEnumerable<IConstraint> constraints, IEnumerable<ISourceCode> sources);
    }
}