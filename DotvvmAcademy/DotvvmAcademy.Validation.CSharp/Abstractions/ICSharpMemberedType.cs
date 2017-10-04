using System.Collections.Generic;

namespace DotvvmAcademy.Validation.CSharp.Abstractions
{
    /// <summary>
    /// A C# type that can contain members i.e. a class, a struct or an interface.
    /// </summary>
    public interface ICSharpMemberedType : ICSharpAllowsAccessModifier, ICSharpAllowsGenericParameters
    {
        ICSharpIndexer Indexer(IEnumerable<ICSharpTypeDescriptor> parameters);

        ICSharpMethod Method();

        ICSharpProperty Property();
    }
}