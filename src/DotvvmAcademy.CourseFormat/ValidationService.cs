using DotvvmAcademy.Validation.CSharp.Unit;
using DotvvmAcademy.Validation.Dothtml.Unit;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using System.Collections.Immutable;
using System.Threading.Tasks;

namespace DotvvmAcademy.CourseFormat
{
    internal class ValidationService
    {
        public async Task<ImmutableArray<ICodeTaskDiagnostic>> Validate(CodeTask task, string userCode)
        {
            var globalsType = task.Id.Language == "cs" ? typeof(ICSharpProject) : typeof(IDothtmlView);
            var script = CSharpScript.Create(task.ValidationScript, globalsType: globalsType);
            var runner = script.CreateDelegate();

        }
    }
}