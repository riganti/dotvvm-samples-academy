using DotVVM.Framework.Compilation;
using DotVVM.Framework.Compilation.ControlTree;
using DotVVM.Framework.Compilation.Parser.Dothtml.Parser;
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
        private readonly IMetaConverter<ISymbol, MemberInfo> symbolConverter;

        public ValidationTreeResolver(
            ValidationControlResolver controlResolver,
            ValidationTreeBuilder treeBuilder,
            ValidationTypeDescriptorFactory descriptorFactory,
            ValidationControlTypeFactory typeFactory,
            IMetaConverter<ISymbol, MemberInfo> symbolConverter)
            : base(controlResolver, treeBuilder)
        {
            this.descriptorFactory = descriptorFactory;
            this.typeFactory = typeFactory;
            this.symbolConverter = symbolConverter;
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
                var type = (Type)symbolConverter.Convert(validationType.TypeSymbol).Single();
                if (type.IsEnum)
                {
                    return Enum.Parse(type, value);
                }

                return Convert.ChangeType(value, type);
            }

            throw new NotSupportedException($"ITypeDescriptor type '{propertyType.GetType()}' is not supported.");
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