using System.Collections.Generic;

namespace DotvvmAcademy.Validation.Dothtml.Unit
{
    /// <summary>
    /// A dothtml binding e.g. value binding, command binding, etc.
    /// </summary>
    public interface IDothtmlBinding
    {
        void Kind(DothtmlBindingKind kind);

        void Value(IEnumerable<object> allowedValues);
    }
}