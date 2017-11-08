using System.Collections.Generic;

namespace DotvvmAcademy.Validation.CSharp.UnitValidation.Abstractions
{

    /// <summary>
    /// A C# method.
    /// </summary>
    public interface ICSharpMethod : ICSharpAllowsAccessModifier, ICSharpAllowsAbstractModifier, ICSharpAllowsAsyncModifier, ICSharpAllowsStaticModifier, ICSharpAllowsVirtualModifier, ICSharpObject, ICSharpAllowsOverrideModifier
    {
        CSharpTypeDescriptor ReturnType { get; set; }
    }
}