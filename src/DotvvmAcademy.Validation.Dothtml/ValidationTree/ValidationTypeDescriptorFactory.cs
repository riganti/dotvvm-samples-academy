using DotVVM.Framework.Compilation.ControlTree;
using DotVVM.Framework.Compilation.ControlTree.Resolved;
using DotVVM.Framework.Compilation.Parser.Binding.Parser;
using DotvvmAcademy.Meta;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Concurrent;

namespace DotvvmAcademy.Validation.Dothtml.ValidationTree
{
    public class ValidationTypeDescriptorFactory
    {
        private readonly ConcurrentDictionary<ITypeSymbol, ValidationTypeDescriptor> cache
            = new ConcurrentDictionary<ITypeSymbol, ValidationTypeDescriptor>();

        private readonly ICSharpCompilationAccessor compilationAccessor;
        private readonly ISymbolNameBuilder symbolNameBuilder;

        public ValidationTypeDescriptorFactory(ICSharpCompilationAccessor compilationAccessor, ISymbolNameBuilder symbolNameBuilder)
        {
            this.compilationAccessor = compilationAccessor;
            this.symbolNameBuilder = symbolNameBuilder;
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

            return cache.GetOrAdd(typeSymbol, s => new ValidationTypeDescriptor(this, compilationAccessor, symbolNameBuilder, s));
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

            var symbol = compilationAccessor.Compilation.GetTypeByMetadataName(metadataName);
            if (symbol == null)
            {
                return Create(compilationAccessor.Compilation.CreateErrorTypeSymbol(
                    container: compilationAccessor.Compilation.GlobalNamespace,
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