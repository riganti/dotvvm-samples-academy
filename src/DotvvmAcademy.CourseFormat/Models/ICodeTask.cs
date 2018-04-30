using System.Threading.Tasks;

namespace DotvvmAcademy.CourseFormat.Models
{
    public interface ICodeTask
    {
        string Language { get; }

        string SourceText { get; }

        Task<ICodeTaskDiagnostic> Validate(string userCode);
    }
}