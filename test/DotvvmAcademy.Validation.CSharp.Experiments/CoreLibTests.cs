using DotvvmAcademy.Meta;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DotvvmAcademy.Validation.CSharp.Experiments
{
    [TestClass]
    public class CoreLibTests
    {
        [TestMethod]
        public void NecessaryDependenciesTest()
        {
            var options = new CSharpCompilationOptions(
                outputKind: OutputKind.DynamicallyLinkedLibrary);
            var compilation = CSharpCompilation.Create(
                assemblyName: nameof(NecessaryDependenciesTest),
                syntaxTrees: Enumerable.Empty<CSharpSyntaxTree>(),
                references: new[]
                {
                    MetadataReferencer.FromName("netstandard"),
                    MetadataReferencer.FromName("System.Private.CoreLib"),
                    MetadataReferencer.FromName("System.Runtime"),
                    MetadataReferencer.FromName("DotVVM.Framework")
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
