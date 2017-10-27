using System.Collections.Generic;

namespace DotvvmAcademy.Validation.CSharp.Abstractions
{
    /// <summary>
    /// A C# type that can be constructed i.e. a class or a struct.
    /// </summary>
    public interface ICSharpConstructibleType : ICSharpMemberedType
    {
        ICSharpConstructor Constructor(IEnumerable<CSharpTypeDescriptor> parameters);

        void ConversionOperator(CSharpTypeDescriptor parameterType, CSharpTypeDescriptor returnType);

        ICSharpDelegate Delegate(string name);

        ICSharpEnum Enum(string name);

        ICSharpEvent Event(string name);

        ICSharpField Field(string name);

        ICSharpInterface Interface(string name);

        ICSharpMethod Operator(string operationName);

        ICSharpStruct Struct(string name);
    }
}