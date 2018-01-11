using System.IO;
using System.Threading.Tasks;

namespace DotvvmAcademy.Validation.CSharp.AssemblyAnalysis
{
    public interface IAssemblyRewriter
    {
        Task Rewrite(Stream source, Stream target);
    }
}