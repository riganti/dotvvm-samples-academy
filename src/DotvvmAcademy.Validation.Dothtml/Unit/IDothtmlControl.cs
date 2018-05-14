using DotVVM.Framework.Binding;
using System.Collections.Generic;

namespace DotvvmAcademy.Validation.Dothtml.Unit
{
    /// <summary>
    /// A dothtml control
    /// </summary>
    public interface IDothtmlControl
    {
        IDothtmlControl this[int index] { get; }

        IDothtmlControl GetControl(string path);
        
        void ValidateBinding<TBinding>(DotvvmProperty property, IEnumerable<string> acceptedValues);

        void ValidateValue(DotvvmProperty property, IEnumerable<object> acceptedValues);
    }
}