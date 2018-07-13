using Microsoft.CodeAnalysis.CSharp;
using System;

namespace DotvvmAcademy.Validation.Dothtml.ValidationTree
{
    internal class ValidationPropertyDescriptorFactory
    {
        private readonly ValidationTypeDescriptorFactory descriptorFactory;

        public ValidationPropertyDescriptorFactory(
            CSharpCompilation compilation,
            ValidationTypeDescriptorFactory descriptorFactory)
        {
            Compilation = compilation;
            this.descriptorFactory = descriptorFactory;
        }

        public CSharpCompilation Compilation { get; }
    }
}