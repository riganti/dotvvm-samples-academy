using System;
using DotVVM.Framework.Compilation;
using DotVVM.Framework.Compilation.ControlTree;
using DotVVM.Framework.Configuration;

namespace DotvvmAcademy.Validation.Dothtml.ValidationTree
{
    internal class ValidationControlResolver : ControlResolverBase
    {
        private readonly ValidationTypeDescriptorFactory descriptorFactory;
        private readonly ValidationControlTypeFactory typeFactory;
        private readonly ValidationControlMetadataFactory metadataFactory;

        public ValidationControlResolver(
            DotvvmMarkupConfiguration configuration,
            ValidationTypeDescriptorFactory descriptorFactory,
            ValidationControlTypeFactory typeFactory,
            ValidationControlMetadataFactory metadataFactory)
            : base(configuration)
        {
            this.descriptorFactory = descriptorFactory;
            this.typeFactory = typeFactory;
            this.metadataFactory = metadataFactory;
        }

        public override IControlResolverMetadata BuildControlMetadata(IControlType type)
        {
            return metadataFactory.Create(type);
        }

        public override IControlResolverMetadata ResolveControl(ITypeDescriptor descriptor)
        {
            var validationDescriptor = descriptorFactory.Convert(descriptor);
            var controlType = new ValidationControlType(validationDescriptor, null, null);
            return ResolveControl(controlType);
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