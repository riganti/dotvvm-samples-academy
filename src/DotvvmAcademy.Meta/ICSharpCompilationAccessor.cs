using Microsoft.CodeAnalysis.CSharp;

namespace DotvvmAcademy.Meta
{
    public interface ICSharpCompilationAccessor
    {
        CSharpCompilation Compilation { get; set; }
    }
}