using System.Collections.Generic;

namespace DotvvmAcademy.Validation.CSharp.Abstractions
{
    /// <summary>
    /// A C# type that can contain members i.e. a class, a struct or an interface.
    /// </summary>
    public interface ICSharpMemberedType : ICSharpAllowsAccessModifier, ICSharpAllowsGenericParameters
    {
        ICSharpIndexer Indexer(IEnumerable<CSharpTypeDescriptor> parameters);

        ICSharpMethod Method(string name, IEnumerable<CSharpTypeDescriptor> parameters);

        ICSharpProperty Property(string name);
    }
}