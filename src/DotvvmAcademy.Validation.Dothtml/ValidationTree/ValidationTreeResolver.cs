using DotVVM.Framework.Compilation;
using DotVVM.Framework.Compilation.ControlTree;
using DotVVM.Framework.Compilation.Parser.Dothtml.Parser;
using DotVVM.Framework.ResourceManagement;
using DotVVM.Framework.Utils;
using DotvvmAcademy.Meta;
using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Reflection;

namespace DotvvmAcademy.Validation.Dothtml.ValidationTree
{
    public class ValidationTreeResolver : ControlTreeResolverBase
    {
        private readonly ValidationTypeDescriptorFactory descriptorFactory;
        private readonly ValidationControlTypeFactory typeFactory;
        private readonly MetaConverter converter;
        private readonly IControlBuilderFactory controlBuilderFactory;

        public ValidationTreeResolver(
            ValidationControlResolver controlResolver,
            ValidationTreeBuilder treeBuilder,
            DotvvmResourceRepository resourceRepo,
            ValidationTypeDescriptorFactory descriptorFactory,
            ValidationControlTypeFactory typeFactory,
            MetaConverter converter,
            IControlBuilderFactory controlBuilderFactory)
            : base(controlResolver, treeBuilder, resourceRepo)
        {
            this.descriptorFactory = descriptorFactory;
            this.typeFactory = typeFactory;
            this.converter = converter;
            this.controlBuilderFactory = controlBuilderFactory;
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
            if (propertyType is ValidationTypeDescriptor validationType)
            {
                var type = (Type)converter.ToReflection(validationType.TypeSymbol).Single();
                return ReflectionUtils.ConvertValue(value, type);
            }
            else
            {
                throw new NotSupportedException($"ITypeDescriptor type '{propertyType.GetType()}' is not supported.");
            }
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

        protected override IAbstractControlBuilderDescriptor? ResolveMasterPage(
            string currentFile,
            IAbstractDirective masterPageDirective)
        {
            try
            {
                return controlBuilderFactory.GetControlBuilder(masterPageDirective.Value).descriptor;
            }
            catch (Exception e)
            {
                // The resolver should not just crash on an invalid directive
                masterPageDirective.DothtmlNode!.AddError(e.Message);
                return null;
            }
        }
    }
}
