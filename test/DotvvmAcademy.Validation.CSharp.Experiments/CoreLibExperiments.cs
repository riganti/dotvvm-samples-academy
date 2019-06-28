using DotvvmAcademy.Meta;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System.Linq;
using Xunit;

namespace DotvvmAcademy.Validation.CSharp.Experiments
{
    public class CoreLibExperiments
    {
        [Fact]
        public void NecessaryDependenciesExperiment()
        {
            var options = new CSharpCompilationOptions(
                outputKind: OutputKind.DynamicallyLinkedLibrary);
            var compilation = CSharpCompilation.Create(
                assemblyName: nameof(NecessaryDependenciesExperiment),
                syntaxTrees: Enumerable.Empty<CSharpSyntaxTree>(),
                references: new[]
                {
                    RoslynReference.FromName("netstandard"),
                    RoslynReference.FromName("System.Private.CoreLib"),
                    RoslynReference.FromName("System.Runtime"),
                    RoslynReference.FromName("DotVVM.Framework")
                },
                options: options);
            var @enum = compilation.GetTypeByMetadataName("System.Enum");
            var markupOptions = compilation.GetTypeByMetadataName("DotVVM.Framework.Controls.MarkupOptionsAttribute");
            var mappingMode = compilation.GetTypeByMetadataName("DotVVM.Framework.Controls.MappingMode");
            Assert.Equal(SymbolKind.NamedType, @enum.Kind);
            Assert.Equal(SymbolKind.NamedType, markupOptions.BaseType.Kind);
            Assert.Equal(SymbolKind.NamedType, mappingMode.BaseType.Kind);
        }
    }
}