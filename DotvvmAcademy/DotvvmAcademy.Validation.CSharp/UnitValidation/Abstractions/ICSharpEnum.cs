using System.Collections.Generic;

namespace DotvvmAcademy.Validation.CSharp.UnitValidation.Abstractions
{
    /// <summary>
    /// A C# enum.
    /// </summary>
    public interface ICSharpEnum : ICSharpAllowsAccessModifier, ICSharpObject
    {
        IList<string> Members { get; set; }
    }
}