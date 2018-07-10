using Microsoft.CodeAnalysis.CSharp;
using System;

namespace DotvvmAcademy.Validation.Dothtml.ValidationTree
{
    internal class ValidationPropertyDescriptorFactory
    {
        private readonly ValidationTypeDescriptorFactory typeFactory;

        public ValidationPropertyDescriptorFactory(
            CSharpCompilation compilation,
            ValidationTypeDescriptorFactory typeFactory)
        {
            Compilation = compilation;
            this.typeFactory = typeFactory;
        }

        public CSharpCompilation Compilation { get; }
    }
}