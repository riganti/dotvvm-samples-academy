using DotvvmAcademy.Meta.Syntax;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DotvvmAcademy.Meta.Tests
{
    [TestClass]
    public class NameParserTests
    {
        [TestMethod]
        public void BasicParserTest()
        {
            var lexer = new NameLexer("System.Collections.Generic.List`1[string]::ToString");
            var parser = new NameParser(lexer);
            var name = parser.Parse();
        }
    }
}