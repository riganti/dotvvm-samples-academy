using Microsoft.CodeAnalysis;

namespace DotvvmAcademy.Meta
{
    public interface ITypedConstantExtractor
    {
        object Extract(TypedConstant constant);
    }
}