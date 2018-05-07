using System.Collections.Immutable;
using System.Threading.Tasks;

namespace DotvvmAcademy.CourseFormat
{
    public interface ICodeTask
    {
        string Code { get; }

        CodeTaskId Id { get; }

        Task<ImmutableArray<ICodeTaskDiagnostic>> Validate(string userCode);
    }
}