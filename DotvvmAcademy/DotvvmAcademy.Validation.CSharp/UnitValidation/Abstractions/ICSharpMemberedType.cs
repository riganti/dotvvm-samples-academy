using System.Collections.Generic;

namespace DotvvmAcademy.Validation.CSharp.UnitValidation.Abstractions
{
    /// <summary>
    /// A C# type that can contain members i.e. a class, a struct or an interface.
    /// </summary>
    public interface ICSharpMemberedType : ICSharpAllowsAccessModifier, ICSharpObject, ICSharpType
    {
        ICSharpIndexer GetIndexer(IEnumerable<CSharpTypeDescriptor> parameters);

        ICSharpMethod GetMethod(string name, IEnumerable<CSharpGenericParameterDescriptor> genericParameters, IEnumerable<CSharpTypeDescriptor> parameters);

        ICSharpProperty GetProperty(string name);
    }
}