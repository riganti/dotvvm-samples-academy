using System;

namespace DotvvmAcademy.Meta.Syntax
{
    public abstract class NameNode
    {
        public static NameNode Parse(string source)
        {
            var lexer = new NameLexer(source);
            var parser = new NameParser(lexer);
            return parser.Parse();
        }

        internal abstract TResult Accept<TResult>(NameNodeVisitor<TResult> visitor);
    }
}