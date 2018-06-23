using DotVVM.Framework.Compilation;
using DotVVM.Framework.Compilation.ControlTree;
using DotVVM.Framework.Compilation.Parser.Binding.Parser;
using DotVVM.Framework.Compilation.Parser.Dothtml.Parser;
using System;
using System.Collections.Generic;

namespace DotvvmAcademy.Validation.Dothtml.ValidationTree
{
    internal class ValidationTreeBuilder : IAbstractTreeBuilder
    {
        public void AddChildControl(IAbstractContentNode control, IAbstractControl child)
        {
            throw new NotImplementedException();
        }

        public bool AddProperty(IAbstractControl control, IAbstractPropertySetter setter, out string error)
        {
            throw new NotImplementedException();
        }

        public IAbstractBaseTypeDirective BuildBaseTypeDirective(DothtmlDirectiveNode directive, BindingParserNode nameSyntax)
        {
            throw new NotImplementedException();
        }

        public IAbstractBinding BuildBinding(BindingParserOptions bindingOptions, IDataContextStack dataContext, DothtmlBindingNode node, IPropertyDescriptor property)
        {
            throw new NotImplementedException();
        }

        public IAbstractControl BuildControl(IControlResolverMetadata metadata, DothtmlNode node, IDataContextStack dataContext)
        {
            throw new NotImplementedException();
        }

        public IAbstractDirective BuildDirective(DothtmlDirectiveNode node)
        {
            throw new NotImplementedException();
        }

        public IAbstractImportDirective BuildImportDirective(DothtmlDirectiveNode node, BindingParserNode aliasSyntax, BindingParserNode nameSyntax)
        {
            throw new NotImplementedException();
        }

        public IAbstractPropertyBinding BuildPropertyBinding(IPropertyDescriptor property, IAbstractBinding binding, DothtmlAttributeNode sourceAttributeNode)
        {
            throw new NotImplementedException();
        }

        public IAbstractPropertyControl BuildPropertyControl(IPropertyDescriptor property, IAbstractControl control, DothtmlElementNode wrapperElementNode)
        {
            throw new NotImplementedException();
        }

        public IAbstractPropertyControlCollection BuildPropertyControlCollection(IPropertyDescriptor property, IEnumerable<IAbstractControl> controls, DothtmlElementNode wrapperElementNode)
        {
            throw new NotImplementedException();
        }

        public IAbstractPropertyTemplate BuildPropertyTemplate(IPropertyDescriptor property, IEnumerable<IAbstractControl> templateControls, DothtmlElementNode wrapperElementNode)
        {
            throw new NotImplementedException();
        }

        public IAbstractPropertyValue BuildPropertyValue(IPropertyDescriptor property, object value, DothtmlNode sourceAttributeNode)
        {
            throw new NotImplementedException();
        }

        public IAbstractServiceInjectDirective BuildServiceInjectDirective(DothtmlDirectiveNode node, SimpleNameBindingParserNode nameSyntax, BindingParserNode typeSyntax)
        {
            throw new NotImplementedException();
        }

        public IAbstractTreeRoot BuildTreeRoot(IControlTreeResolver controlTreeResolver, IControlResolverMetadata metadata, DothtmlRootNode node, IDataContextStack dataContext, IReadOnlyDictionary<string, IReadOnlyList<IAbstractDirective>> directives)
        {
            throw new NotImplementedException();
        }

        public IAbstractViewModelDirective BuildViewModelDirective(DothtmlDirectiveNode directive, BindingParserNode nameSyntax)
        {
            throw new NotImplementedException();
        }
    }
}