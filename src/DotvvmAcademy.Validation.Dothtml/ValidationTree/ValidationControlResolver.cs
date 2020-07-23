using DotVVM.Framework.Binding.Expressions;
using DotVVM.Framework.Compilation;
using DotVVM.Framework.Compilation.ControlTree;
using DotVVM.Framework.Compilation.Parser;
using DotvvmAcademy.Meta;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System;
using System.Collections.Concurrent;
using System.Collections.Immutable;
using System.Linq;

namespace DotvvmAcademy.Validation.Dothtml.ValidationTree
{
    public class ValidationControlResolver : IControlResolver
    {
        private readonly ImmutableDictionary<string, BindingParserOptions> bindingOptions = GetDefaultBindingOptions();
        private readonly Compilation compilation;
        private readonly ValidationTypeDescriptorFactory descriptorFactory;
        private readonly ValidationControlMetadataFactory metadataFactory;
        private readonly ValidationPropertyFactory propertyFactory;
        private readonly ConcurrentDictionary<string, INamespaceSymbol> namespaces
            = new ConcurrentDictionary<string, INamespaceSymbol>();

        private readonly ValidationControlTypeFactory typeFactory;

        public ValidationControlResolver(
            Compilation compilation,
            ValidationTypeDescriptorFactory descriptorFactory,
            ValidationControlTypeFactory typeFactory,
            ValidationControlMetadataFactory metadataFactory,
            ValidationPropertyFactory propertyFactory)
        {
            this.compilation = compilation;
            this.descriptorFactory = descriptorFactory;
            this.typeFactory = typeFactory;
            this.metadataFactory = metadataFactory;
            this.propertyFactory = propertyFactory;
        }

        public IControlResolverMetadata BuildControlMetadata(IControlType type)
        {
            return metadataFactory.Create(type);
        }

        public IPropertyDescriptor FindProperty(IControlResolverMetadata metadata, string name)
        {
            if (metadata.TryGetProperty(name, out var property))
            {
                return property;
            }

            if (name.Contains('.'))
            {
                return GetAttachedProperty(name);
            }

            var group = metadata.PropertyGroups
                .FirstOrDefault(g => name.StartsWith(g.Prefix, StringComparison.OrdinalIgnoreCase));
            if (!group.Equals(default(PropertyGroupMatcher)))
            {
                return propertyFactory.CreateGrouped(group.PropertyGroup, name.Substring(group.Prefix.Length));
            }

            return null;
        }

        private IPropertyDescriptor GetAttachedProperty(string name)
        {
            var segments = name.Split('.');
            var control = namespaces.Values.SelectMany(n => n.GetTypeMembers(segments[0])).SingleOrDefault();
            if (control == null)
            {
                return null;
            }
            var metadata = metadataFactory.Create(control);
            return metadata.Properties.SingleOrDefault(p => p.Name == segments[1]);
        }

        public ImmutableDictionary<string, INamespaceSymbol> GetRegisteredNamespaces()
        {
            return namespaces.ToImmutableDictionary();
        }

        public void RegisterNamespace(string prefix, string @namespace, string assembly)
        {
            var assemblySymbol = compilation.References
                .Select(compilation.GetAssemblyOrModuleSymbol)
                .OfType<IAssemblySymbol>()
                .Single(a => a.MetadataName == assembly);

            // TODO: Replace with actual meta name analysis
            var segments = @namespace.Split('.');
            var current = assemblySymbol.GlobalNamespace;
            foreach (var segment in segments)
            {
                current = current.GetMembers(segment).OfType<INamespaceSymbol>().SingleOrDefault();
                if (current == null)
                {
                    throw new ArgumentException($"Namespace '{@namespace}' could " +
                        $"not be found in assembly '{assembly}'.");
                }
            }
            RegisterNamespace(prefix, current);
        }

        public void RegisterNamespace(string prefix, INamespaceSymbol namespaceSymbol)
        {
            namespaces.GetOrAdd(prefix, namespaceSymbol);
        }

        public BindingParserOptions ResolveBinding(string bindingType)
        {
            if (bindingOptions.TryGetValue(bindingType, out var options))
            {
                return options;
            }

            return null;
        }

        public IControlResolverMetadata ResolveControl(string tagPrefix, string tagName, out object[] ctorArguments)
        {
            ctorArguments = new object[0];
            if (tagPrefix == null)
            {
                return metadataFactory.Create(DotvvmTypes.HtmlGenericControl);
            }

            if (!namespaces.TryGetValue(tagPrefix, out var namespaceSymbol))
            {
                return null;
            }

            var typeSymbol = namespaceSymbol.GetTypeMembers(tagName).SingleOrDefault();
            if (typeSymbol == null)
            {
                return null;
            }

            return metadataFactory.Create(typeSymbol);
        }

        public IControlResolverMetadata ResolveControl(IControlType type)
        {
            return metadataFactory.Create(type);
        }

        public IControlResolverMetadata ResolveControl(ITypeDescriptor descriptor)
        {
            var validationDescriptor = descriptorFactory.Convert(descriptor);
            var controlType = new ValidationControlType(validationDescriptor, null, null);
            return ResolveControl(controlType);
        }

        private static ImmutableDictionary<string, BindingParserOptions> GetDefaultBindingOptions()
        {
            var builder = ImmutableDictionary.CreateBuilder<string, BindingParserOptions>();
            builder.Add(
                ParserConstants.ValueBinding,
                BindingParserOptions.Create(typeof(ValueBindingExpression<>)));
            builder.Add(
                ParserConstants.CommandBinding,
                BindingParserOptions.Create(typeof(CommandBindingExpression<>)));
            builder.Add(
                ParserConstants.ControlPropertyBinding,
                BindingParserOptions.Create(typeof(ControlPropertyBindingExpression<>), "_control"));
            builder.Add(
                ParserConstants.ControlCommandBinding,
                BindingParserOptions.Create(typeof(ControlCommandBindingExpression<>), "_control"));
            builder.Add(
                ParserConstants.ResourceBinding,
                BindingParserOptions.Create(typeof(ResourceBindingExpression<>)));
            builder.Add(
                ParserConstants.StaticCommandBinding,
                BindingParserOptions.Create(typeof(StaticCommandBindingExpression<>)));
            return builder.ToImmutable();
        }
    }
}
