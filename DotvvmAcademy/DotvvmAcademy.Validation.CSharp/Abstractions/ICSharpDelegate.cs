using System.Collections.Generic;

namespace DotvvmAcademy.Validation.CSharp.Abstractions
{
    /// <summary>
    /// A C# delegate.
    /// </summary>
    public interface ICSharpDelegate : ICSharpAllowsAccessModifier
    {
        void Parameters(IEnumerable<ICSharpTypeDescriptor> parameters);

        void ReturnType(ICSharpTypeDescriptor type);
    }
}