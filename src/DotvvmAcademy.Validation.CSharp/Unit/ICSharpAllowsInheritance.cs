using System.Collections.Generic;

namespace DotvvmAcademy.Validation.CSharp.Unit
{
    /// <summary>
    /// A C# member or type that can inherit from other types and that can be inherited from by other types.
    /// </summary>
    public interface ICSharpAllowsInheritance : ICSharpObject
    {
        IList<CSharpTypeDescriptor> BaseTypes { get; set; }
    }
}