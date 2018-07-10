using System.Collections.Generic;
using DotVVM.Framework.Compilation;
using DotVVM.Framework.Compilation.ControlTree;
using DotVVM.Framework.Compilation.Parser.Dothtml.Parser;

namespace DotvvmAcademy.Validation.Dothtml.ValidationTree
{
    internal class ValidationTreeResolver : ControlTreeResolverBase
    {
        public ValidationTreeResolver(
            IControlResolver controlResolver,
            IAbstractTreeBuilder treeBuilder)
            : base(controlResolver, treeBuilder)
        {
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
            throw new System.NotImplementedException();
        }

        protected override IDataContextStack CreateDataContextTypeStack(ITypeDescriptor viewModelType, IDataContextStack parentDataContextStack = null, IReadOnlyList<NamespaceImport> imports = null, IReadOnlyList<BindingExtensionParameter> extensionParameters = null)
        {
            throw new System.NotImplementedException();
        }
    }
}