using DotVVM.Framework.Compilation.ControlTree;
using DotVVM.Framework.Compilation.ControlTree.Resolved;
using DotVVM.Framework.Compilation.Parser.Binding.Parser;
using DotvvmAcademy.Meta;
using DotvvmAcademy.Meta.Syntax;
using Microsoft.CodeAnalysis;
using System;
using System.Collections.Concurrent;

namespace DotvvmAcademy.Validation.Dothtml.ValidationTree
{
    public class ValidationTypeDescriptorFactory
    {
        private readonly ConcurrentDictionary<ITypeSymbol, ValidationTypeDescriptor> cache
            = new ConcurrentDictionary<ITypeSymbol, ValidationTypeDescriptor>();
        private readonly Compilation compilation;

        public ValidationTypeDescriptorFactory(Compilation compilation)
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
            if (name is MemberAccessBindingParserNode member)
            {
                return Create(member);
            }
            else if (name is AssemblyQualifiedNameBindingParserNode qualifiedName
                && qualifiedName.TypeName is MemberAccessBindingParserNode nestedMember)
            {
                return Create(nestedMember);
            }

            return Create(name.ToDisplayString());
        }

        public ValidationTypeDescriptor Create(MemberAccessBindingParserNode name)
        {
            return Create(name.ToDisplayString());
        }

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

        public ValidationTypeDescriptor Create(Type type)
        {
            return Create(type.FullName);
        }
    }
}
