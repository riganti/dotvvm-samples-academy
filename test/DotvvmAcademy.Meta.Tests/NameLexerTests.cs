using DotvvmAcademy.Meta.Syntax;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DotvvmAcademy.Meta.Tests
{
    [TestClass]
    public class NameLexerTests
    {
        [TestMethod]
        public void BasicLexerTest()
        {
            var lexer = new NameLexer("System.Collections.Generic.List`1[System.String]");
        }
    }
}
