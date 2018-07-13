using System;
using System.Collections.Concurrent;
using DotVVM.Framework.Compilation.ControlTree;
using Microsoft.CodeAnalysis;

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

        public ValidationControlType Create(ITypeSymbol symbol)
        {
            return cache.GetOrAdd(symbol, t => new ValidationControlType(descriptorFactory.Create(t)));
        }

        public ValidationControlType Create(
            ITypeDescriptor descriptor,
            string virtualPath = null,
            ITypeDescriptor dataContextRequirement = null)
        {
            var validationDescriptor = descriptorFactory.Convert(descriptor);
            return cache.GetOrAdd(validationDescriptor.TypeSymbol, t => {
                return new ValidationControlType(
                    type: validationDescriptor,
                    virtualPath: virtualPath,
                    dataContextRequirement: descriptorFactory.Convert(dataContextRequirement));
            });
        }
    }
}