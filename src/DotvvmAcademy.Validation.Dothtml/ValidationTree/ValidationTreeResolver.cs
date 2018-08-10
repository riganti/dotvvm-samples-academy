using DotVVM.Framework.Compilation;
using DotVVM.Framework.Compilation.ControlTree;
using DotVVM.Framework.Compilation.Parser.Dothtml.Parser;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace DotvvmAcademy.Validation.Dothtml.ValidationTree
{
    public class ValidationTreeResolver : ControlTreeResolverBase
    {
        private readonly ValidationTypeDescriptorFactory descriptorFactory;
        private readonly ValidationControlTypeFactory typeFactory;

        public ValidationTreeResolver(
            ValidationControlResolver controlResolver,
            ValidationTreeBuilder treeBuilder,
            ValidationTypeDescriptorFactory descriptorFactory,
            ValidationControlTypeFactory typeFactory)
            : base(controlResolver, treeBuilder)
        {
            this.descriptorFactory = descriptorFactory;
            this.typeFactory = typeFactory;
        }

        protected override IAbstractBinding CompileBinding(
            DothtmlBindingNode node,
            BindingParserOptions bindingOptions,
            IDataContextStack context,
            IPropertyDescriptor property)
        {
            return treeBuilder.BuildBinding(bindingOptions, context, node, property);
        }

        protected override object ConvertValue(string value, ITypeDescriptor propertyType)
        {
            return value;
        }

        protected override IControlType CreateControlType(ITypeDescriptor wrapperType, string virtualPath)
        {
            return typeFactory.Create(wrapperType, virtualPath);
        }

        protected override IDataContextStack CreateDataContextTypeStack(
            ITypeDescriptor viewModelType,
            IDataContextStack parentDataContextStack = null,
            IReadOnlyList<NamespaceImport> imports = null,
            IReadOnlyList<BindingExtensionParameter> extensionParameters = null)
        {
            return new ValidationDataContextStack(
                dataContextType: descriptorFactory.Convert(viewModelType),
                parent: (parentDataContextStack as ValidationDataContextStack),
                namespaceImports: imports?.ToImmutableArray() ?? default,
                extensionParameters: extensionParameters?.ToImmutableArray() ?? default);
        }
    }
}