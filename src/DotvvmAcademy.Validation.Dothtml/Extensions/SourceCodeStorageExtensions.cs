using DotVVM.Framework.Compilation.ControlTree;
using DotvvmAcademy.Validation.Dothtml;

namespace DotvvmAcademy.Validation
{
    public static class SourceCodeStorageExtensions
    {
        public static DothtmlSourceCode GetSourceCode(this SourceCodeStorage storage, IAbstractTreeRoot root)
        {
            if (storage.Sources.TryGetValue(root.FileName, out var sourceCode))
            {
                return (DothtmlSourceCode)sourceCode;
            }
            return null;
        }
    }
}
