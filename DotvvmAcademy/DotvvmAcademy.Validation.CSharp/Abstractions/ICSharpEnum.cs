using System.Collections.Generic;

namespace DotvvmAcademy.Validation.CSharp.Abstractions
{
    /// <summary>
    /// A C# enum.
    /// </summary>
    public interface ICSharpEnum : ICSharpAllowsAccessModifier
    {
        IList<string> Members { get; set; }
    }
}