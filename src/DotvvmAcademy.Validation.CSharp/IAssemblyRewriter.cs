using System.IO;
using System.Threading.Tasks;

namespace DotvvmAcademy.Validation.CSharp
{
    public interface IAssemblyRewriter
    {
        Task Rewrite(Stream source, Stream target);
    }
}