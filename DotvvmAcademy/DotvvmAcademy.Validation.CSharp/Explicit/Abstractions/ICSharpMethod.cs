using System.Collections.Generic;

namespace DotvvmAcademy.Validation.CSharp.Abstractions
{

    /// <summary>
    /// A C# method.
    /// </summary>
    public interface ICSharpMethod : ICSharpAllowsAccessModifier, ICSharpAllowsAbstractModifier, ICSharpAllowsAsyncModifier, ICSharpAllowsStaticModifier, ICSharpAllowsVirtualModifier, ICSharpObject
    {
        CSharpTypeDescriptor ReturnType { get; set; }
    }
}