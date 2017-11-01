using System.Collections.Generic;

namespace DotvvmAcademy.Validation.CSharp.Abstractions
{
    /// <summary>
    /// A C# type that can be constructed i.e. a class or a struct.
    /// </summary>
    public interface ICSharpConstructibleType : ICSharpMemberedType, ICSharpObject
    {
        ICSharpConstructor GetConstructor(IEnumerable<CSharpTypeDescriptor> parameters);

        ICSharpConversionOperator GetConversionOperator(CSharpTypeDescriptor parameterType, CSharpTypeDescriptor returnType);

        ICSharpDelegate GetDelegate(string name, IEnumerable<CSharpGenericParameterDescriptor> genericParameters);

        ICSharpEnum GetEnum(string name);

        ICSharpEvent GetEvent(string name);

        ICSharpField GetField(string name);

        ICSharpInterface GetInterface(string name, IEnumerable<CSharpGenericParameterDescriptor> genericParameters);

        ICSharpMethod Operator(string operationName);

        ICSharpStruct Struct(string name, IEnumerable<CSharpGenericParameterDescriptor> genericParameters);
    }
}