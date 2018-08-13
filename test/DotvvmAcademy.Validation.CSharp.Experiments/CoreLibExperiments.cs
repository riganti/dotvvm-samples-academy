using DotvvmAcademy.Meta;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace DotvvmAcademy.Validation.CSharp.Experiments
{
    [TestClass]
    public class CoreLibExperiments
    {
        [TestMethod]
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
            Assert.AreEqual(@enum.Kind, SymbolKind.NamedType);
            Assert.AreEqual(markupOptions.BaseType.Kind, SymbolKind.NamedType);
            Assert.AreEqual(mappingMode.BaseType.Kind, SymbolKind.NamedType);
        }
    }
}