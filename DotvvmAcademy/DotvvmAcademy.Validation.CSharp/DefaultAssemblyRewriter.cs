using System;
using System.IO;
using System.Threading.Tasks;

namespace DotvvmAcademy.Validation.CSharp
{
    public class DefaultAssemblyRewriter : IAssemblyRewriter
    {
        public Task Rewrite(Stream source, Stream target)
        {
            throw new NotImplementedException();
        }
    }
}