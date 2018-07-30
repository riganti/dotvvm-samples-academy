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

        private readonly ImmutableArray<ParserDothtmlDiagnostic>.Builder diagnostics
            = ImmutableArray.CreateBuilder<ParserDothtmlDiagnostic>();

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

        public ImmutableArray<ParserDothtmlDiagnostic> GetDiagnostics()
            => diagnostics.ToImmutable();

        protected override IAbstractBinding CompileBinding(
                    DothtmlBindingNode node,
            BindingParserOptions bindingOptions,
            IDataContextStack context,
            IPropertyDescriptor property)
            => treeBuilder.BuildBinding(bindingOptions, context, node, property);

        protected override object ConvertValue(string value, ITypeDescriptor propertyType)
        {
            return value;
        }

        protected override IControlType CreateControlType(ITypeDescriptor wrapperType, string virtualPath)
            => typeFactory.Create(wrapperType, virtualPath);

        protected override IDataContextStack CreateDataContextTypeStack(
            ITypeDescriptor viewModelType,
            IDataContextStack parentDataContextStack = null,
            IReadOnlyList<NamespaceImport> imports = null,
            IReadOnlyList<BindingExtensionParameter> extensionParameters = null)
        {
            var immutableImports = imports?.ToImmutableArray()
                ?? default;
            var immutableParameters = extensionParameters?.ToImmutableArray()
                ?? default;
            return new ValidationDataContextStack(
                dataContextType: descriptorFactory.Convert(viewModelType),
                parent: (parentDataContextStack as ValidationDataContextStack),
                namespaceImports: immutableImports,
                extensionParameters: immutableParameters);
        }

        protected override bool LogError(Exception exception, DothtmlNode node)
        {
            diagnostics.Add(new ParserDothtmlDiagnostic(exception.Message, node));
            return true;
        }
    }
}