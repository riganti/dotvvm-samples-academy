using DotVVM.Framework.Compilation;
using DotVVM.Framework.Compilation.ControlTree;
using DotVVM.Framework.Compilation.Parser.Binding.Parser;
using DotVVM.Framework.Compilation.Parser.Dothtml.Parser;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace DotvvmAcademy.Validation.Dothtml.ValidationTree
{
    public class ValidationTreeBuilder : IAbstractTreeBuilder
    {
        private readonly ValidationTypeDescriptorFactory descriptorFactory;
        private readonly ValidationPropertyFactory propertyFactory;

        public ValidationTreeBuilder(ValidationTypeDescriptorFactory descriptorFactory, ValidationPropertyFactory propertyFactory)
        {
            this.descriptorFactory = descriptorFactory;
            this.propertyFactory = propertyFactory;
        }

        public void AddChildControl(IAbstractContentNode control, IAbstractControl child)
        {
            ((ValidationContentNode)control).AddChildControl((ValidationControl)child);
        }

        public bool AddProperty(IAbstractControl control, IAbstractPropertySetter setter, out string error)
        {
            ((ValidationControl)control).AddProperty((ValidationPropertySetter)setter);
            error = null;
            return true;
        }

        public IAbstractBaseTypeDirective BuildBaseTypeDirective(DothtmlDirectiveNode directive, BindingParserNode nameSyntax)
        {
            return new ValidationBaseTypeDirective(directive, nameSyntax, descriptorFactory.Create(nameSyntax));
        }

        public IAbstractBinding BuildBinding(
            BindingParserOptions bindingOptions,
            IDataContextStack dataContext,
            DothtmlBindingNode node,
            IPropertyDescriptor property)
        {
            return new ValidationBinding(node, bindingOptions.BindingType, (ValidationDataContextStack)dataContext);
        }

        public IAbstractControl BuildControl(IControlResolverMetadata metadata, DothtmlNode node, IDataContextStack dataContext)
        {
            return new ValidationControl(node, (ValidationControlMetadata)metadata, dataContext);
        }

        public IAbstractDirective BuildDirective(DothtmlDirectiveNode node)
        {
            return new ValidationDirective(node);
        }

        public IAbstractImportDirective BuildImportDirective(
            DothtmlDirectiveNode node,
            BindingParserNode aliasSyntax,
            BindingParserNode nameSyntax)
        {
            return new ValidationImportDirective(node, nameSyntax, aliasSyntax);
        }

        public IAbstractPropertyBinding BuildPropertyBinding(
            IPropertyDescriptor property,
            IAbstractBinding binding,
            DothtmlAttributeNode sourceAttributeNode)
        {
            return new ValidationPropertyBinding(
                node: sourceAttributeNode,
                property: propertyFactory.Convert(property),
                binding: (ValidationBinding)binding);
        }

        public IAbstractPropertyControl BuildPropertyControl(
            IPropertyDescriptor property,
            IAbstractControl control,
            DothtmlElementNode wrapperElementNode)
        {
            return new ValidationPropertyControl(
                node: wrapperElementNode,
                property: propertyFactory.Convert(property),
                control: (ValidationControl)control);
        }

        public IAbstractPropertyControlCollection BuildPropertyControlCollection(
            IPropertyDescriptor property,
            IEnumerable<IAbstractControl> controls,
            DothtmlElementNode wrapperElementNode)
        {
            return new ValidationPropertyControlCollection(
                node: wrapperElementNode,
                property: propertyFactory.Convert(property),
                controls: controls.Cast<ValidationControl>().ToImmutableArray());
        }

        public IAbstractPropertyTemplate BuildPropertyTemplate(
            IPropertyDescriptor property,
            IEnumerable<IAbstractControl> templateControls,
            DothtmlElementNode wrapperElementNode)
        {
            return new ValidationPropertyTemplate(
                node: wrapperElementNode,
                property: propertyFactory.Convert(property),
                content: templateControls.Cast<ValidationControl>().ToImmutableArray());
        }

        public IAbstractPropertyValue BuildPropertyValue(IPropertyDescriptor property, object value, DothtmlNode sourceAttributeNode)
        {
            return new ValidationPropertyValue(
                node: sourceAttributeNode,
                property: propertyFactory.Convert(property),
                value: value);
        }

        public IAbstractServiceInjectDirective BuildServiceInjectDirective(
            DothtmlDirectiveNode node,
            SimpleNameBindingParserNode nameSyntax,
            BindingParserNode typeSyntax)
        {
            return new ValidationServiceInjectDirective(
                node: node,
                nameSyntax: nameSyntax,
                typeSyntax: typeSyntax,
                type: descriptorFactory.Create(typeSyntax));
        }

        public IAbstractTreeRoot BuildTreeRoot(
            IControlTreeResolver controlTreeResolver,
            IControlResolverMetadata metadata,
            DothtmlRootNode node,
            IDataContextStack dataContext,
            IReadOnlyDictionary<string, IReadOnlyList<IAbstractDirective>> directives,
            IAbstractControlBuilderDescriptor? masterPage)
        {
            var immutableDirectives = directives
                .SelectMany(p => p.Value)
                .Cast<ValidationDirective>()
                .ToImmutableArray();
            var root = new ValidationTreeRoot(
                node: node,
                metadata: (ValidationControlMetadata)metadata,
                dataContext: dataContext,
                directives: immutableDirectives,
                masterPage: masterPage);
            return root;
        }

        public IAbstractViewModelDirective BuildViewModelDirective(
            DothtmlDirectiveNode directive,
            BindingParserNode nameSyntax)
        {
            return new ValidationViewModelDirective(directive, nameSyntax, descriptorFactory.Create(nameSyntax));
        }

        public IAbstractDirective BuildViewModuleDirective(
            DothtmlDirectiveNode directiveNode,
            string modulePath,
            string resourceName)
        {
            return new ValidationViewModuleDirective(directiveNode, modulePath, resourceName);
        }
    }
}
