using System.Collections.Generic;

namespace DotvvmAcademy.Validation.CSharp.Abstractions
{
    /// <summary>
    /// A C# type that can contain members i.e. a class, a struct or an interface.
    /// </summary>
    public interface ICSharpMemberedType : ICSharpAllowsAccessModifier
    {
        ICSharpIndexer GetIndexer(IEnumerable<CSharpTypeDescriptor> parameters);

        ICSharpMethod GetMethod(string name, IEnumerable<CSharpTypeDescriptor> parameters, IEnumerable<CSharpGenericParameterDescriptor> genericParameters);

        ICSharpProperty GetProperty(string name);
    }
}