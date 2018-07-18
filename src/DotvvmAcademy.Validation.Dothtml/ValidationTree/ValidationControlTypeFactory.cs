using DotVVM.Framework.Compilation.ControlTree;
using Microsoft.CodeAnalysis;
using System.Collections.Concurrent;

namespace DotvvmAcademy.Validation.Dothtml.ValidationTree
{
    internal class ValidationControlTypeFactory
    {
        private readonly ConcurrentDictionary<ITypeSymbol, ValidationControlType> cache
            = new ConcurrentDictionary<ITypeSymbol, ValidationControlType>();

        private readonly ValidationTypeDescriptorFactory descriptorFactory;

        public ValidationControlTypeFactory(ValidationTypeDescriptorFactory descriptorFactory)
        {
            this.descriptorFactory = descriptorFactory;
        }

        public ValidationControlType Create(
            string metadataName,
            string virtualPath = null,
            string dataContextRequirement = null)
            => Create(
                descriptor: descriptorFactory.Create(metadataName),
                virtualPath: virtualPath,
                dataContextRequirement: descriptorFactory.Create(dataContextRequirement));

        public ValidationControlType Create(
            ITypeSymbol symbol,
            string virtualPath = null,
            ITypeSymbol dataContextRequirement = null)
            => Create(descriptorFactory.Create(symbol), virtualPath, descriptorFactory.Create(dataContextRequirement));

        public ValidationControlType Create(
            ITypeDescriptor descriptor,
            string virtualPath = null,
            ITypeDescriptor dataContextRequirement = null)
        {
            var validationDescriptor = descriptorFactory.Convert(descriptor);
            return cache.GetOrAdd(validationDescriptor.TypeSymbol, t =>
            {
                return new ValidationControlType(
                    type: validationDescriptor,
                    virtualPath: virtualPath,
                    dataContextRequirement: descriptorFactory.Convert(dataContextRequirement));
            });
        }
    }
}