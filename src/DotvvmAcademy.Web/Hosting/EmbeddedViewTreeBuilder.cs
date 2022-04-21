using DotVVM.Framework.Compilation;
using DotVVM.Framework.Compilation.Binding;
using DotVVM.Framework.Compilation.ControlTree;
using DotVVM.Framework.Compilation.ControlTree.Resolved;
using DotVVM.Framework.Compilation.Parser.Binding.Parser;
using DotVVM.Framework.Compilation.Parser.Dothtml.Parser;
using DotVVM.Framework.Compilation.ViewCompiler;
using DotVVM.Framework.Utils;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq.Expressions;
using System.Reflection;

namespace DotvvmAcademy.Web.Hosting
{
    public class EmbeddedViewTreeBuilder : IAbstractTreeBuilder
    {
        private readonly IAbstractTreeBuilder builder;
        private readonly CompiledAssemblyCache compiledAssemblyCache;
        private readonly ExtensionMethodsCache extensionMethodsCache;

        public EmbeddedViewTreeBuilder(
            IAbstractTreeBuilder builder,
            CompiledAssemblyCache compiledAssemblyCache,
            ExtensionMethodsCache extensionMethodsCache)
        {
            this.builder = builder;
            this.compiledAssemblyCache = compiledAssemblyCache;
            this.extensionMethodsCache = extensionMethodsCache;
        }

        public Assembly AdditionalAssembly { get; set; }

        public void AddChildControl(IAbstractContentNode control, IAbstractControl child)
        {
            builder.AddChildControl(control, child);
        }

        public bool AddProperty(IAbstractControl control, IAbstractPropertySetter setter, out string error)
        {
            return builder.AddProperty(control, setter, out error);
        }

        public IAbstractBaseTypeDirective BuildBaseTypeDirective(DothtmlDirectiveNode directive, BindingParserNode nameSyntax)
        {
            var type = ResolveTypeNameDirective(directive, nameSyntax);
            return new ResolvedBaseTypeDirective(nameSyntax, type) { DothtmlNode = directive };
        }

        public IAbstractBinding BuildBinding(BindingParserOptions bindingOptions, IDataContextStack dataContext, DothtmlBindingNode node, IPropertyDescriptor property)
        {
            return builder.BuildBinding(bindingOptions, dataContext, node, property);
        }

        public IAbstractControl BuildControl(IControlResolverMetadata metadata, DothtmlNode node, IDataContextStack dataContext)
        {
            return builder.BuildControl(metadata, node, dataContext);
        }

        public IAbstractDirective BuildDirective(DothtmlDirectiveNode node)
        {
            return builder.BuildDirective(node);
        }

        public IAbstractImportDirective BuildImportDirective(DothtmlDirectiveNode node, BindingParserNode aliasSyntax, BindingParserNode nameSyntax)
        {
            return builder.BuildImportDirective(node, aliasSyntax, nameSyntax);
        }

        public IAbstractPropertyBinding BuildPropertyBinding(IPropertyDescriptor property, IAbstractBinding binding, DothtmlAttributeNode sourceAttributeNode)
        {
            return builder.BuildPropertyBinding(property, binding, sourceAttributeNode);
        }

        public IAbstractPropertyControl BuildPropertyControl(IPropertyDescriptor property, IAbstractControl control, DothtmlElementNode wrapperElementNode)
        {
            return builder.BuildPropertyControl(property, control, wrapperElementNode);
        }

        public IAbstractPropertyControlCollection BuildPropertyControlCollection(IPropertyDescriptor property, IEnumerable<IAbstractControl> controls, DothtmlElementNode wrapperElementNode)
        {
            return builder.BuildPropertyControlCollection(property, controls, wrapperElementNode);
        }

        public IAbstractPropertyTemplate BuildPropertyTemplate(IPropertyDescriptor property, IEnumerable<IAbstractControl> templateControls, DothtmlElementNode wrapperElementNode)
        {
            return builder.BuildPropertyTemplate(property, templateControls, wrapperElementNode);
        }

        public IAbstractPropertyValue BuildPropertyValue(IPropertyDescriptor property, object value, DothtmlNode sourceAttributeNode)
        {
            return builder.BuildPropertyValue(property, value, sourceAttributeNode);
        }

        public IAbstractServiceInjectDirective BuildServiceInjectDirective(DothtmlDirectiveNode node, SimpleNameBindingParserNode nameSyntax, BindingParserNode typeSyntax)
        {
            return builder.BuildServiceInjectDirective(node, nameSyntax, typeSyntax);
        }

        public IAbstractTreeRoot BuildTreeRoot(IControlTreeResolver controlTreeResolver, IControlResolverMetadata metadata, DothtmlRootNode node, IDataContextStack dataContext, IReadOnlyDictionary<string, IReadOnlyList<IAbstractDirective>> directives, IAbstractControlBuilderDescriptor masterPage)
        {
            return builder.BuildTreeRoot(controlTreeResolver, metadata, node, dataContext, directives, masterPage);
        }

        public IAbstractViewModelDirective BuildViewModelDirective(DothtmlDirectiveNode directive, BindingParserNode nameSyntax)
        {
            var type = ResolveTypeNameDirective(directive, nameSyntax);
            return new ResolvedViewModelDirective(nameSyntax, type) { DothtmlNode = directive };
        }

        public IAbstractDirective BuildViewModuleDirective(DothtmlDirectiveNode directiveNode, string modulePath, string resourceName)
        {
            return builder.BuildViewModuleDirective(directiveNode, modulePath, resourceName);
        }

        private TypeRegistry CreateRegistry()
        {
            return new TypeRegistry(
                compiledAssemblyCache,
                ImmutableDictionary.Create<string, Expression>()
                .Add("object", TypeRegistry.CreateStatic(typeof(Object)))
                .Add("bool", TypeRegistry.CreateStatic(typeof(Boolean)))
                .Add("byte", TypeRegistry.CreateStatic(typeof(Byte)))
                .Add("char", TypeRegistry.CreateStatic(typeof(Char)))
                .Add("short", TypeRegistry.CreateStatic(typeof(Int16)))
                .Add("int", TypeRegistry.CreateStatic(typeof(Int32)))
                .Add("long", TypeRegistry.CreateStatic(typeof(Int64)))
                .Add("ushort", TypeRegistry.CreateStatic(typeof(UInt16)))
                .Add("uint", TypeRegistry.CreateStatic(typeof(UInt32)))
                .Add("ulong", TypeRegistry.CreateStatic(typeof(UInt64)))
                .Add("decimal", TypeRegistry.CreateStatic(typeof(Decimal)))
                .Add("double", TypeRegistry.CreateStatic(typeof(Double)))
                .Add("float", TypeRegistry.CreateStatic(typeof(Single)))
                .Add("string", TypeRegistry.CreateStatic(typeof(String)))
                .Add("Object", TypeRegistry.CreateStatic(typeof(Object)))
                .Add("Boolean", TypeRegistry.CreateStatic(typeof(Boolean)))
                .Add("Byte", TypeRegistry.CreateStatic(typeof(Byte)))
                .Add("Char", TypeRegistry.CreateStatic(typeof(Char)))
                .Add("Int16", TypeRegistry.CreateStatic(typeof(Int16)))
                .Add("Int32", TypeRegistry.CreateStatic(typeof(Int32)))
                .Add("Int64", TypeRegistry.CreateStatic(typeof(Int64)))
                .Add("UInt16", TypeRegistry.CreateStatic(typeof(UInt16)))
                .Add("UInt32", TypeRegistry.CreateStatic(typeof(UInt32)))
                .Add("UInt64", TypeRegistry.CreateStatic(typeof(UInt64)))
                .Add("Decimal", TypeRegistry.CreateStatic(typeof(Decimal)))
                .Add("Double", TypeRegistry.CreateStatic(typeof(Double)))
                .Add("Single", TypeRegistry.CreateStatic(typeof(Single)))
                .Add("String", TypeRegistry.CreateStatic(typeof(String))),
            ImmutableArray.Create<Func<string, Expression>>()
                .Add(type => TypeRegistry.CreateStatic(compiledAssemblyCache.FindType(type)))
                .Add(type => TypeRegistry.CreateStatic(compiledAssemblyCache.FindType("System." + type)))
                .Add(type => TypeRegistry.CreateStatic(AdditionalAssembly.GetType(type))));
        }

        private Expression ParseDirectiveExpression(DothtmlDirectiveNode directive, BindingParserNode expressionSyntax)
        {
            var registry = CreateRegistry();
            if (expressionSyntax is AssemblyQualifiedNameBindingParserNode)
            {
                var assemblyQualifiedName = expressionSyntax as AssemblyQualifiedNameBindingParserNode;
                expressionSyntax = assemblyQualifiedName.TypeName;
            }

            var visitor = new ExpressionBuildingVisitor(registry, new MemberExpressionFactory(extensionMethodsCache))
            {
                ResolveOnlyTypeName = true,
                Scope = null
            };

            try
            {
                return visitor.Visit(expressionSyntax);
            }
            catch (Exception ex)
            {
                directive.AddError($"{expressionSyntax.ToDisplayString()} is not a valid type or namespace: {ex.Message}");
                return null;
            }
        }

        private ResolvedTypeDescriptor ResolveTypeNameDirective(DothtmlDirectiveNode directive, BindingParserNode nameSyntax)
        {
            if (ParseDirectiveExpression(directive, nameSyntax) is StaticClassIdentifierExpression expression)
            {
                return new ResolvedTypeDescriptor(expression.Type);
            }

            directive.AddError($"Could not resolve type '{nameSyntax.ToDisplayString()}'.");
            return null;
        }
    }
}
