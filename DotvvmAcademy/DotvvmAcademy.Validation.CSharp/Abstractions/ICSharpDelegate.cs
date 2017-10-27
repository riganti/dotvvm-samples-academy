using System.Collections.Generic;

namespace DotvvmAcademy.Validation.CSharp.Abstractions
{
    /// <summary>
    /// A C# delegate.
    /// </summary>
    public interface ICSharpDelegate : ICSharpAllowsAccessModifier
    {
        void Parameters(IEnumerable<CSharpTypeDescriptor> parameters);

        void ReturnType(CSharpTypeDescriptor type);
    }
}