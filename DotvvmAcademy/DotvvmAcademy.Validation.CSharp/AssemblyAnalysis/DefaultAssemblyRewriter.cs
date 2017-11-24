using Mono.Cecil;
using System.IO;
using System.Threading.Tasks;

namespace DotvvmAcademy.Validation.CSharp.AssemblyAnalysis
{
    public class DefaultAssemblyRewriter : IAssemblyRewriter
    {
        public Task Rewrite(Stream source, Stream target)
        {
            return Task.Run(() =>
            {
                var assembly = AssemblyDefinition.ReadAssembly(source);
                foreach(var module in assembly.Modules)
                {

                }
                assembly.Write(target);
            });
        }
    }
}