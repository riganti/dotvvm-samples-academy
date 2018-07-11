using System;
using DotVVM.Framework.Compilation;
using DotVVM.Framework.Compilation.ControlTree;
using DotVVM.Framework.Configuration;

namespace DotvvmAcademy.Validation.Dothtml.ValidationTree
{
    internal class ValidationControlResolver : ControlResolverBase
    {
        private readonly ValidationTypeDescriptorFactory typeFactory;

        public ValidationControlResolver(
            DotvvmMarkupConfiguration configuration,
            ValidationTypeDescriptorFactory typeFactory)
            : base(configuration)
        {
            this.typeFactory = typeFactory;
        }

        public override IControlResolverMetadata BuildControlMetadata(IControlType type)
        {
            return new ValidationControlMetadata((ValidationControlType)type);
        }

        public override IControlResolverMetadata ResolveControl(ITypeDescriptor controlType)
        {
            
            return new ValidationControlMetadata(typeFactory.Convert(controlType));
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