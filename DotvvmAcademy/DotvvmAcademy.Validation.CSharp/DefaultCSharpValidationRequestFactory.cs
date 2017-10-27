using DotvvmAcademy.Validation.CSharp.Abstractions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System;
using System.Collections.Immutable;

namespace DotvvmAcademy.Validation.CSharp
{
    public class DefaultCSharpValidationRequestFactory : ICSharpValidationRequestFactory
    {
        private readonly ImmutableArray<MetadataReference> defaultReferences = new ImmutableArray<MetadataReference>
        {
            MetadataReference.CreateFromFile(typeof(object).Assembly.Location)
        };

        public CSharpValidationRequest CreateRequest(string source, string validationMethodName)
        {
            var request = new CSharpValidationRequest();
            var tree = CSharpSyntaxTree.ParseText(source) as CSharpSyntaxTree;
            request.ValidationUnits.Add(new CSharpValidationUnit(tree, validationMethodName));
            var compilation = CSharpCompilation.Create(
                assemblyName: Guid.NewGuid().ToString(),
                syntaxTrees: new[] { tree },
                references: defaultReferences,
                options: new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));
            request.Compilation = compilation;
            return request;
        }
    }
}