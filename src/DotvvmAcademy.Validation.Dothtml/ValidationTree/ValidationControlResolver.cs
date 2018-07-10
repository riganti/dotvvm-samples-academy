using System;
using DotVVM.Framework.Compilation;
using DotVVM.Framework.Compilation.ControlTree;
using DotVVM.Framework.Configuration;

namespace DotvvmAcademy.Validation.Dothtml.ValidationTree
{
    internal class ValidationControlResolver : ControlResolverBase
    {
        public ValidationControlResolver(DotvvmMarkupConfiguration configuration) : base(configuration)
        {
        }

        public override IControlResolverMetadata BuildControlMetadata(IControlType type)
        {
            throw new NotImplementedException();
        }

        public override IControlResolverMetadata ResolveControl(ITypeDescriptor controlType)
        {
            throw new NotImplementedException();
        }

        protected override IControlType FindCompiledControl(string tagName, string namespaceName, string assemblyName)
        {
            throw new NotImplementedException();
        }

        protected override IPropertyDescriptor FindGlobalProperty(string name)
        {
            throw new NotImplementedException();
        }

        protected override IControlType FindMarkupControl(string file)
        {
            throw new NotImplementedException();
        }
    }
}