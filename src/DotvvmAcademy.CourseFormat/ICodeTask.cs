using System.Threading.Tasks;

namespace DotvvmAcademy.CourseFormat
{
    public interface ICodeTask
    {
        CodeTaskId Id { get; }

        string Language { get; }

        string SourceText { get; }

        Task<ICodeTaskDiagnostic> Validate(string userCode);
    }
}