using System.Collections.Immutable;

namespace DotvvmAcademy.Validation.Dothtml.Unit
{
    /// <summary>
    /// A dothtml property.
    /// </summary>
    public interface IDothtmlProperty
    {
        void ValidateBinding(DothtmlBindingKind kind, ImmutableArray<string> acceptableValues);

        void ValidateValue(ImmutableArray<object> acceptedValues);
    }
}