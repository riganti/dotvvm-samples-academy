using DotVVM.Framework.Compilation;
using System;

namespace DotvvmAcademy.Validation.Dothtml
{
    internal class FakeControlBuilderFactory : IControlBuilderFactory
    {
        public (ControlBuilderDescriptor descriptor, Lazy<IControlBuilder> builder) GetControlBuilder(string virtualPath)
        {
            throw new NotSupportedException("Markup controls are not supported in Dothtml Validation.");
        }
    }
}