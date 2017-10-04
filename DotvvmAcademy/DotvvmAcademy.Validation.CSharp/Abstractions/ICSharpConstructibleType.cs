using System.Collections.Generic;

namespace DotvvmAcademy.Validation.CSharp.Abstractions
{
    /// <summary>
    /// A C# type that can be constructed i.e. a class or a struct.
    /// </summary>
    public interface ICSharpConstructibleType : ICSharpMemberedType
    {
        ICSharpConstructor Constructor(IEnumerable<ICSharpTypeDescriptor> parameters);

        ICSharpProperty ConversionOperator(ICSharpTypeDescriptor parameterType, ICSharpTypeDescriptor returnType);

        ICSharpDelegate Delegate(string name);

        ICSharpEnum Enum(string name);

        ICSharpEvent Event(string name);

        ICSharpField Field(string name);

        ICSharpIndexer Indexer(IEnumerable<ICSharpTypeDescriptor> parameters);

        ICSharpMemberedType Interface(string name);

        ICSharpMethod Operator(string operationName);

        ICSharpMemberedType Struct(string name);
    }
}