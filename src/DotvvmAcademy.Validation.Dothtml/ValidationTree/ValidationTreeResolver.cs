using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using DotVVM.Framework.Compilation;
using DotVVM.Framework.Compilation.ControlTree;
using DotVVM.Framework.Compilation.ControlTree.Resolved;
using DotVVM.Framework.Compilation.Parser.Dothtml.Parser;

namespace DotvvmAcademy.Validation.Dothtml.ValidationTree
{
    internal class ValidationTreeResolver : ControlTreeResolverBase
    {
        private readonly ValidationTypeDescriptorFactory typeFactory;

        public ValidationTreeResolver(
            IControlResolver controlResolver,
            IAbstractTreeBuilder treeBuilder,
            ValidationTypeDescriptorFactory typeFactory)
            : base(controlResolver, treeBuilder)
        {
            this.typeFactory = typeFactory;
        }

        protected override IAbstractBinding CompileBinding(DothtmlBindingNode node, BindingParserOptions bindingOptions, IDataContextStack context, IPropertyDescriptor property)
        {
            throw new System.NotImplementedException();
        }

        protected override object ConvertValue(string value, ITypeDescriptor propertyType)
        {
            throw new System.NotImplementedException();
        }

        protected override IControlType CreateControlType(ITypeDescriptor wrapperType, string virtualPath)
        {
            return new ValidationControlType(typeFactory.Convert(wrapperType), virtualPath, null);
        }

        protected override IDataContextStack CreateDataContextTypeStack(
            ITypeDescriptor viewModelType,
            IDataContextStack parentDataContextStack = null,
            IReadOnlyList<NamespaceImport> imports = null,
            IReadOnlyList<BindingExtensionParameter> extensionParameters = null)
        {
            var immutableImports = imports?.ToImmutableArray() 
                ?? default(ImmutableArray<NamespaceImport>);
            var immutableParameters = extensionParameters?.ToImmutableArray() 
                ?? default(ImmutableArray<BindingExtensionParameter>);
            return new ValidationDataContextStack(
                dataContextType: typeFactory.Convert(viewModelType),
                parent: (parentDataContextStack as ValidationDataContextStack),
                namespaceImports: immutableImports,
                extensionParameters: immutableParameters);
        }
    }
}