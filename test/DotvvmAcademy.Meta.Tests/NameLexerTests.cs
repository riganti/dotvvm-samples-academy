using DotvvmAcademy.Meta.Syntax;
using Xunit;

namespace DotvvmAcademy.Meta.Tests
{
    public class NameLexerTests
    {
        [Fact]
        public void Lex_Identifier_GenerateIdentifierToken()
        {
            var source = "TestType";
            var expected = NameFactory.IdentifierToken(source);

            var sut = new NameLexer(source);
            var actual = sut.Lex();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Lex_Identifier_GeneratesEnd()
        {
            var source = "TestType";
            var expected = new NameToken(NameTokenKind.End, source, 8, 8);

            var sut = new NameLexer(source);
            sut.Lex();
            var actual = sut.Lex();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Lex_Generic_GeneratesIdentifierToken()
        {
            var source = "TestType`2";
            var expected = new NameToken(NameTokenKind.Identifier, source, 0, 8);

            var sut = new NameLexer(source);
            var actual = sut.Lex();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Lex_Generic_GeneratesBacktickToken()
        {
            var source = "TestType`2";
            var expected = new NameToken(NameTokenKind.Backtick, source, 8, 9);

            var sut = new NameLexer(source);
            sut.Lex();
            var actual = sut.Lex();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Lex_Generic_GeneratesNumericLiteralToken()
        {
            var source = "TestType`2";
            var expected = new NameToken(NameTokenKind.NumericLiteral, source, 9, 10);

            var sut = new NameLexer(source);
            sut.Lex();
            sut.Lex();
            var actual = sut.Lex();

            Assert.Equal(expected, actual);
        }
    }
}