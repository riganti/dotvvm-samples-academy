using DotvvmAcademy.Validation.Dothtml;
using DotvvmAcademy.Validation.Dothtml.ValidationTree;

namespace DotvvmAcademy.Validation
{
    public static class SourceCodeStorageExtensions
    {
        public static DothtmlSourceCode GetSourceCode(this SourceCodeStorage storage, ValidationTreeRoot root)
        {
            if (storage.Sources.TryGetValue(root.FileName, out var sourceCode))
            {
                return (DothtmlSourceCode)sourceCode;
            }
            return null;
        }
    }
}