using DotvvmAcademy.Validation.CSharp.Unit;
using DotvvmAcademy.Validation.CSharp.Unit.Abstractions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace DotvvmAcademy.Validation.CSharp
{
    public static class CSharpValidationUtilities
    {
        public static CSharpValidationRequest CreateRequest(string source, string[] otherSources = null)
        {
            var treeCount = 1 + (otherSources?.Length ?? 0);
            var trees = new SyntaxTree[treeCount];
            trees[0] = CSharpSyntaxTree.ParseText(source);
            if (otherSources != null)
            {
                for (int i = 0; i < otherSources.Length; i++)
                {
                    trees[i + 1] = CSharpSyntaxTree.ParseText(otherSources[i]);
                }
            }
            var compilation = Compile(trees);
            var request = new CSharpValidationRequest();
            request.FileTable.Add(Guid.NewGuid().ToString(), trees[0]);
            request.Compilation = compilation;
            return request;
        }

        public static CSharpValidationRequest CreateRequest(string[] sources, string[] sourceNames)
        {
            var trees = sources.Select(s => CSharpSyntaxTree.ParseText(s)).ToArray();
            var compilation = Compile(trees);
            var request = new CSharpValidationRequest();
            for (int i = 0; i < sourceNames.Length; i++)
            {
                request.FileTable.Add(sourceNames[i], trees[i]);
            }
            request.Compilation = compilation;
            return request;
        }

        public static ICSharpUnitValidationRunner CreateRunner()
        {
            var collection = new ServiceCollection();
            collection.AddCSharpUnitValidation();
            return new DefaultCSharpUnitValidationRunner(collection.BuildServiceProvider());
        }

        public static ICSharpValidator CreateValidator()
        {
            var collection = new ServiceCollection();
            collection.AddCSharpValidationAnalyzers();
            var validator = new DefaultCSharpValidator(collection.BuildServiceProvider());
            return validator;
        }

        private static CSharpCompilation Compile(SyntaxTree[] trees)
        {
            return CSharpCompilation.Create(
                assemblyName: Guid.NewGuid().ToString(),
                syntaxTrees: trees,
                references: new[] { MetadataReference.CreateFromFile(typeof(object).Assembly.Location) },
                options: new CSharpCompilationOptions(
                    outputKind: OutputKind.DynamicallyLinkedLibrary,
                    optimizationLevel: OptimizationLevel.Release));
        }
    }
}