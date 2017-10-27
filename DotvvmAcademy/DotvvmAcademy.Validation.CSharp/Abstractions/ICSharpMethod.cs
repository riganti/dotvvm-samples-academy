using System.Collections.Generic;

namespace DotvvmAcademy.Validation.CSharp.Abstractions
{

    /// <summary>
    /// A C# method.
    /// </summary>
    public interface ICSharpMethod : ICSharpAllowsAccessModifier, ICSharpAllowsAbstractModifier, ICSharpAllowsAsyncModifier, ICSharpAllowsGenericParameters, ICSharpAllowsStaticModifier, ICSharpAllowsVirtualModifier
    {
        void ReturnType(CSharpTypeDescriptor type);

        void Parameters(IEnumerable<CSharpTypeDescriptor> parameters);
    }
}