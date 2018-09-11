using System.Collections.Generic;
using System.Collections.Immutable;

namespace DotvvmAcademy.Meta.Syntax
{
    public class NameParser
    {
        private readonly List<NameToken> tokens = new List<NameToken>();
        private int position;

        public NameParser(NameLexer lexer)
        {
            Lexer = lexer;
        }

        protected NameToken Current => Peek();

        protected NameLexer Lexer { get; }

        public virtual NameNode Parse()
        {
            var typeName = ParseTypeName();
            if (Current.Kind == NameTokenKind.ColonColon)
            {
                return ParseMemberName(typeName);
            }

            return typeName;
        }

        protected TNode AddError<TNode>(TNode node, NameDiagnostic diagnostic) where TNode : NameNode
        {
            var diagnostics = node.Diagnostics.Add(diagnostic);
            return (TNode)node.SetDiagnostics(diagnostics);
        }

        protected void Advance()
        {
            position++;
        }

        protected NameToken Peek(int delta = 0)
        {
            if (position + delta >= tokens.Count)
            {
                var peekCount = position + delta - tokens.Count + 1;
                for (int i = 0; i < peekCount; i++)
                {
                    tokens.Add(Lexer.Lex());
                }
            }

            return tokens[position + delta];
        }

        private NameToken CreateMissingToken(NameTokenKind kind)
        {
            return new NameToken(kind, Lexer.Source, -1, -1, true);
        }

        private ArrayTypeNameNode ParseArrayTypeName(NameNode elementType)
        {
            var diagnosticsBuilder = ImmutableArray.CreateBuilder<NameDiagnostic>();
            NameToken openBracket;
            if (Current.Kind == NameTokenKind.OpenBracket)
            {
                openBracket = Current;
                Advance();
            }
            else
            {
                openBracket = CreateMissingToken(NameTokenKind.OpenBracket);
                diagnosticsBuilder.Add(new NameDiagnostic("An opening bracket was expected."));
            }

            var commaBuilder = ImmutableArray.CreateBuilder<NameToken>();
            while (Current.Kind == NameTokenKind.Comma)
            {
                commaBuilder.Add(Current);
                Advance();
            }

            NameToken closeBracket;
            if (Current.Kind == NameTokenKind.CloseBracket)
            {
                closeBracket = Current;
                Advance();
            }
            else
            {
                closeBracket = CreateMissingToken(NameTokenKind.CloseBracket);
                diagnosticsBuilder.Add(new NameDiagnostic("A closing bracket was expected."));
            }

            return new ArrayTypeNameNode(
                elementType: elementType,
                openBracketToken: openBracket,
                closeBracketToken: closeBracket,
                commaTokens: commaBuilder.ToImmutable(),
                diagnostics: diagnosticsBuilder.ToImmutable());
        }

        private NameNode ParseBoundTypeName()
        {
            var name = ParseNestedTypeName();
            if (Current.Kind == NameTokenKind.OpenBracket && Peek(1).Kind == NameTokenKind.Identifier)
            {
                return new ConstructedTypeNameNode(name, ParseTypeArgumentList());
            }

            return name;
        }

        private IdentifierNameNode ParseIdentifier()
        {
            if (Current.Kind == NameTokenKind.Identifier)
            {
                var name = new IdentifierNameNode(Current);
                Advance();
                return name;
            }

            var diagnostic = new NameDiagnostic("An identifier was expected.");
            return new IdentifierNameNode(
                identifierToken: CreateMissingToken(NameTokenKind.Identifier),
                diagnostics: ImmutableArray.Create(diagnostic));
        }

        private MemberNameNode ParseMemberName(NameNode type)
        {
            var diagnosticBuilder = ImmutableArray.CreateBuilder<NameDiagnostic>();
            NameToken colonColon;
            if (Current.Kind == NameTokenKind.ColonColon)
            {
                colonColon = Current;
                Advance();
            }
            else
            {
                diagnosticBuilder.Add(new NameDiagnostic("A '::' token was expected."));
                colonColon = CreateMissingToken(NameTokenKind.ColonColon);
            }

            return new MemberNameNode(
                type: type,
                member: ParseIdentifier(),
                colonColonToken: colonColon,
                diagnostics: diagnosticBuilder.ToImmutable());
        }

        private NameNode ParseNestedTypeName()
        {
            NameNode name = ParseQualifiedName();
            while (Current.Kind == NameTokenKind.Plus)
            {
                var plusToken = Current;
                Advance();
                var right = ParseSimpleName();
                name = new NestedTypeNameNode(name, right, plusToken);
            }
            return name;
        }

        private NameNode ParseQualifiedName()
        {
            NameNode name = ParseSimpleName();
            while (Current.Kind == NameTokenKind.Dot)
            {
                var dotToken = Current;
                Advance();
                var right = ParseSimpleName();
                name = new QualifiedNameNode(name, right, dotToken);
            }
            return name;
        }

        private SimpleNameNode ParseSimpleName()
        {
            SimpleNameNode name = ParseIdentifier();
            if (name.IdentifierToken.IsMissing || Current.Kind != NameTokenKind.Backtick)
            {
                return name;
            }

            var backtickToken = Current;
            Advance();
            if (Current.Kind != NameTokenKind.NumericLiteral)
            {
                var arity = CreateMissingToken(NameTokenKind.NumericLiteral);
                var diagnostic = new NameDiagnostic("A numerical value of arity is missing.");
                return new GenericNameNode(name.IdentifierToken, backtickToken, arity);
            }

            name = new GenericNameNode(name.IdentifierToken, backtickToken, Current);
            Advance();
            return name;
        }

        private TypeArgumentListNode ParseTypeArgumentList()
        {
            var diagnosticBuilder = ImmutableArray.CreateBuilder<NameDiagnostic>();
            var argumentBuilder = ImmutableArray.CreateBuilder<NameNode>();
            var separatorBuilder = ImmutableArray.CreateBuilder<NameToken>();
            NameToken openBracket;
            if (Current.Kind == NameTokenKind.OpenBracket)
            {
                openBracket = Current;
                Advance();
            }
            else
            {
                openBracket = CreateMissingToken(NameTokenKind.OpenBracket);
                diagnosticBuilder.Add(new NameDiagnostic("An opening bracket was expected."));
            }

            while (Current.Kind == NameTokenKind.Identifier)
            {
                argumentBuilder.Add(ParseTypeName());
                if (Current.Kind == NameTokenKind.Comma)
                {
                    separatorBuilder.Add(Current);
                    Advance();
                }
            }

            NameToken closeBracket;
            if (Current.Kind == NameTokenKind.CloseBracket)
            {
                closeBracket = Current;
                Advance();
            }
            else
            {
                closeBracket = CreateMissingToken(NameTokenKind.CloseBracket);
                diagnosticBuilder.Add(new NameDiagnostic("A closing bracket was expected."));
            }

            return new TypeArgumentListNode(
                arguments: argumentBuilder.ToImmutable(),
                commaTokens: separatorBuilder.ToImmutable(),
                openBracketToken: openBracket,
                closeBracketToken: closeBracket,
                diagnostics: diagnosticBuilder.ToImmutable());
        }

        private NameNode ParseTypeName()
        {
            NameNode name;
            name = ParseBoundTypeName();
            while (true)
            {
                if (Current.Kind == NameTokenKind.Asterisk)
                {
                    name = new PointerTypeNameNode(name, Current);
                    Advance();
                }
                else if (Current.Kind == NameTokenKind.OpenBracket)
                {
                    name = ParseArrayTypeName(name);
                }
                else
                {
                    break;
                }
            }
            return name;
        }
    }
}