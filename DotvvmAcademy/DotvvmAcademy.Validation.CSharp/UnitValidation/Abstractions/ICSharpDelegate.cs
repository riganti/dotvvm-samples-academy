using System.Collections.Generic;

namespace DotvvmAcademy.Validation.CSharp.UnitValidation.Abstractions
{
    /// <summary>
    /// A C# delegate.
    /// </summary>
    public interface ICSharpDelegate : ICSharpAllowsAccessModifier, ICSharpObject
    {
        IList<CSharpTypeDescriptor> Parameters { get; set; }

        IList<CSharpTypeDescriptor> ReturnType { get; set; }
    }
}