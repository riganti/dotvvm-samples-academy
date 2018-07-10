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
            DescriptorIEnumerable = Create(Compilation.GetTypeByMetadataName("System.Collections.Generic.IEnumerable`1"));
        }

        public CSharpCompilation Compilation { get; }

        public ValidationTypeDescriptor Create(ITypeSymbol typeSymbol)
        {
            return cache.GetOrAdd(typeSymbol, s => new ValidationTypeDescriptor(this, s));
        }

        public ValidationTypeDescriptor Create(BindingParserNode name)
        {
            throw new NotImplementedException();
        }
    }
}