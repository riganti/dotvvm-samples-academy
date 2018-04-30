using DotVVM.Framework.Binding;

namespace DotvvmAcademy.Validation.Dothtml.Unit
{
    /// <summary>
    /// A dothtml control
    /// </summary>
    public interface IDothtmlControl
    {
        IDothtmlControl this[int index] { get; }

        IDothtmlProperty GetProperty(DotvvmProperty property);
    }
}