using System.Collections.Generic;

namespace DotvvmAcademy.Validation.CSharp.Abstractions
{
    /// <summary>
    /// A C# namespace.
    /// </summary>
    public interface ICSharpNamespace : ICSharpObject
    {
        ICSharpClass GetClass(string name, IEnumerable<CSharpGenericParameterDescriptor> genericParameters);

        ICSharpDelegate GetDelegate(string name);

        ICSharpEnum GetEnum(string name);

        ICSharpInterface GetInterface(string name, IEnumerable<CSharpGenericParameterDescriptor> genericParameters);

        ICSharpNamespace GetNamespace(string name);

        ICSharpStruct GetStruct(string name, IEnumerable<CSharpGenericParameterDescriptor> genericParameters);
    }
}