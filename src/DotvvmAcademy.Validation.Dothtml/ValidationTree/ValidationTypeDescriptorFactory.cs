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
        public readonly ValidationTypeDescriptor DescriptorIEnumerable;
        
        private ConcurrentDictionary<ITypeSymbol, ValidationTypeDescriptor> cache
            = new ConcurrentDictionary<ITypeSymbol, ValidationTypeDescriptor>();

        public ValidationTypeDescriptorFactory(CSharpCompilation compilation)
        {
            Compilation = compilation;
            DescriptorIEnumerable = Create("System.Collections.Generic.IEnumerable`1");
        }

        public CSharpCompilation Compilation { get; }

        public ValidationTypeDescriptor Create(ITypeSymbol typeSymbol)
        {
            return cache.GetOrAdd(typeSymbol, s => new ValidationTypeDescriptor(this, s));
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
            var symbol = Compilation.GetTypeByMetadataName(metadataName);
            if (symbol == null)
            {
                return Create(Compilation.CreateErrorTypeSymbol(
                    container: Compilation.GlobalNamespace,
                    name: metadataName,
                    arity: 0));
            }
            return Create(symbol);
        }

        public ValidationTypeDescriptor Create(Type type)
        {
            return Create(type.FullName);
        }

        public ValidationTypeDescriptor Convert(ITypeDescriptor other)
        {
            if (other is ResolvedTypeDescriptor resolved)
            {
                return Create(resolved.Type);
            }
            return Create(other.FullName);
        }
    }
}