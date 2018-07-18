using DotVVM.Framework.Compilation.ControlTree;
using DotVVM.Framework.Compilation.ControlTree.Resolved;
using DotVVM.Framework.Compilation.Parser.Binding.Parser;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System;
using System.Collections.Concurrent;

namespace DotvvmAcademy.Validation.Dothtml.ValidationTree
{
    internal class ValidationTypeDescriptorFactory
    {
        private readonly ConcurrentDictionary<ITypeSymbol, ValidationTypeDescriptor> cache
            = new ConcurrentDictionary<ITypeSymbol, ValidationTypeDescriptor>();

        private readonly CSharpCompilation compilation;

        public ValidationTypeDescriptorFactory(CSharpCompilation compilation)
        {
            this.compilation = compilation;
        }

        public ValidationTypeDescriptor Convert(ITypeDescriptor other)
        {
            if (other == null)
            {
                return null;
            }
            if (other is ResolvedTypeDescriptor resolved)
            {
                return Create(resolved.Type);
            }
            return Create(other.FullName);
        }

        public ValidationTypeDescriptor Create(ITypeSymbol typeSymbol)
        {
            if (typeSymbol == null)
            {
                return null;
            }

            return cache.GetOrAdd(typeSymbol, s => new ValidationTypeDescriptor(this, compilation, s));
        }

        public ValidationTypeDescriptor Create(BindingParserNode name)
        {
            if (name is MemberAccessBindingParserNode)
            {
                return Create((MemberAccessBindingParserNode)name);
            }
            else if (name is AssemblyQualifiedNameBindingParserNode qualifiedName
                && qualifiedName.TypeName is MemberAccessBindingParserNode)
            {
                return Create((MemberAccessBindingParserNode)qualifiedName.TypeName);
            }

            return Create(name.ToDisplayString());
        }

        public ValidationTypeDescriptor Create(MemberAccessBindingParserNode name) => Create(name.ToDisplayString());

        public ValidationTypeDescriptor Create(string metadataName)
        {
            if (string.IsNullOrEmpty(metadataName))
            {
                return null;
            }

            var symbol = compilation.GetTypeByMetadataName(metadataName);
            if (symbol == null)
            {
                return Create(compilation.CreateErrorTypeSymbol(
                    container: compilation.GlobalNamespace,
                    name: metadataName,
                    arity: 0));
            }
            return Create(symbol);
        }

        public ValidationTypeDescriptor Create(Type type) => Create(type.FullName);
    }
}