using DotvvmAcademy.Validation.CSharp;
using Microsoft.CodeAnalysis;

namespace DotvvmAcademy.Validation
{
    public static class SourceCodeStorageExtensions
    {
        public static CSharpSourceCode GetSourceCode(this SourceCodeStorage storage, SyntaxTree tree)
        {
            if (storage.Sources.TryGetValue(tree.FilePath, out var source))
            {
                return (CSharpSourceCode)source;
            }

            return null;
        }
    }
}