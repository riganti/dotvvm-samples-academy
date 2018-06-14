using System.Collections.Immutable;
using System.Threading.Tasks;

namespace DotvvmAcademy.CourseFormat
{
    public interface IValidationService
    {
        Task<ImmutableArray<ICodeTaskDiagnostic>> Validate(CodeTaskId id, string code);
    }
}