using System.Collections.Immutable;

namespace DotvvmAcademy.Meta.Syntax
{
    public abstract class NameNode
    {
        public NameNode(NameNodeKind kind, ImmutableArray<NameDiagnostic> diagnostics = default)
        {
            Kind = kind;
            Diagnostics = diagnostics;
        }

        public ImmutableArray<NameDiagnostic> Diagnostics { get; }

        public NameNodeKind Kind { get; }

        public static NameNode Parse(string source)
        {
            var lexer = new NameLexer(source);
            var parser = new NameParser(lexer);
            return parser.Parse();
        }

        public abstract NameNode SetDiagnostics(ImmutableArray<NameDiagnostic> diagnostics);

        public abstract override string ToString();
    }
}