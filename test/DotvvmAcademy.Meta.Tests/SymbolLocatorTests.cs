using DotvvmAcademy.Meta.Syntax;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace DotvvmAcademy.Meta.Tests
{
    [TestClass]
    public class SymbolLocatorTests
    {
        [TestMethod]
        public void BasicLocatorTest()
        {
            var compilation = GetCompilation();
            var lexer = new NameLexer("System.Collections.Generic.List`1");
            var parser = new NameParser(lexer);
            var locator = new SymbolLocator(compilation);
            var node = parser.Parse();
            var symbols = locator.Locate(node);
        }

        private CSharpCompilation GetCompilation()
        {
            var options = new CSharpCompilationOptions(
                outputKind: OutputKind.DynamicallyLinkedLibrary);
            return CSharpCompilation.Create(
                assemblyName: "Test",
                syntaxTrees: Enumerable.Empty<CSharpSyntaxTree>(),
                references: new[]
                {
                    RoslynReference.FromName("netstandard"),
                    RoslynReference.FromName("System.Private.CoreLib"),
                    RoslynReference.FromName("System.Runtime")
                },
                options: options);
        }
    }
}