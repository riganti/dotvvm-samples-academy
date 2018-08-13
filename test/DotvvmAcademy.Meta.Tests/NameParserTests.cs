using DotvvmAcademy.Meta.Syntax;
using Xunit;

namespace DotvvmAcademy.Meta.Tests
{
    public class NameParserTests
    {
        [Fact]
        public void NameParser_MemberName_CreateNode()
        {
            var lexer = new NameLexer("System.Collections.Generic.List`1[string]::ToString");
            var parser = new NameParser(lexer);
            var name = parser.Parse();
        }

        [Fact]
        public void NameParser_MemberNameWithDot_CreateNode()
        {
            var lexer = new NameLexer("System.Int32::.ctor");
            var parser = new NameParser(lexer);
            var name = parser.Parse();
        }

    }
}