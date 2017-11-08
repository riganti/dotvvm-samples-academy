using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;

namespace DotvvmAcademy.Validation.CSharp
{
    public static class CSharpValidationUtilities
    {
        public static CSharpValidationRequest CreateRequest(string source)
        {
            var tree = CSharpSyntaxTree.ParseText(source);
            var compilation = CSharpCompilation.Create(
                assemblyName: Guid.NewGuid().ToString(),
                syntaxTrees: new[] { tree },
                references: new[] { MetadataReference.CreateFromFile(typeof(object).Assembly.Location) },
                options: new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));
            var request = new CSharpValidationRequest();
            request.FileTable.Add(Guid.NewGuid().ToString(), tree);
            request.Compilation = compilation;
            return request;
        }

        public static ICSharpValidator CreateValidator()
        {
            var collection = new ServiceCollection();
            collection.AddCSharpValidationAnalyzers();
            var validator = new DefaultCSharpValidator(collection.BuildServiceProvider());
            return validator;
        }

        public static void RunValidationMethod(CSharpValidationRequest request, MethodInfo methodInfo)
        {
            throw new NotImplementedException();
        }
    }
}