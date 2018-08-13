using DotvvmAcademy.Meta.Syntax;
using Xunit;

namespace DotvvmAcademy.Meta.Tests
{
    public class NameLexerTests
    {
        [Fact]
        public void NameLexer_ConstructedName_GenerateTokens()
        {
            var lexer = new NameLexer("System.Collections.Generic.List`1[System.String]");
            NameToken current;
            do
            {
                current = lexer.Lex();
            }
            while (current.Kind != NameTokenKind.End);
        }
    }
}