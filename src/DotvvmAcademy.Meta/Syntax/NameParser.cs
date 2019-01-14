using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace DotvvmAcademy.Meta.Syntax
{
    internal class NameParser
    {
        private readonly List<NameToken> tokens = new List<NameToken>();
        private List<NameDiagnostic> diagnostics = new List<NameDiagnostic>();
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
            var messages = new List<string>();
            NameToken openBracket;
            if (Current.Kind == NameTokenKind.OpenBracket)
            {
                openBracket = Current;
                Advance();
            }
            else
            {
                openBracket = CreateMissingToken(NameTokenKind.OpenBracket);
                messages.Add("An opening bracket was expected.");
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
                messages.Add("A closing bracket was expected.");
            }

            var result = new ArrayTypeNameNode(
                elementType: elementType,
                openBracketToken: openBracket,
                closeBracketToken: closeBracket,
                commaTokens: commaBuilder.ToImmutable());
            diagnostics.AddRange(messages.Select(m => new NameDiagnostic(result, m)));
            return result;
        }

        private NameNode ParseBoundTypeName()
        {
            var name = ParseNestedTypeName();
            if (Current.Kind != NameTokenKind.OpenBracket || Peek(1).Kind != NameTokenKind.Identifier)
            {
                return name;
            }

            var messages = new List<string>();
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
                messages.Add("An opening bracket was expected.");
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
                messages.Add("A closing bracket was expected.");
            }

            var result = new ConstructedTypeNameNode(
                unboundTypeName: name,
                arguments: argumentBuilder.ToImmutable(),
                commaTokens: separatorBuilder.ToImmutable(),
                openBracketToken: openBracket,
                closeBracketToken: closeBracket);
            diagnostics.AddRange(messages.Select(m => new NameDiagnostic(result, m)));
            return result;
        }

        private IdentifierNameNode ParseIdentifier()
        {
            if (Current.Kind == NameTokenKind.Identifier)
            {
                var name = new IdentifierNameNode(Current);
                Advance();
                return name;
            }
            else
            {
                var name = new IdentifierNameNode(CreateMissingToken(NameTokenKind.Identifier));
                diagnostics.Add(new NameDiagnostic(name, "An identifier was expected."));
                return name;
            }

        }

        private MemberNameNode ParseMemberName(NameNode type)
        {
            if (Current.Kind == NameTokenKind.ColonColon)
            {
                var result = new MemberNameNode(type, ParseIdentifier(), Current);
                Advance();
                return result;
            }
            else
            {
                var result = new MemberNameNode(type, ParseIdentifier(), CreateMissingToken(NameTokenKind.ColonColon));
                diagnostics.Add(new NameDiagnostic(result, "A '::' token was expected."));
                return result;
            }
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
            var name = ParseIdentifier();
            if (Current.Kind != NameTokenKind.Backtick)
            {
                return name;
            }

            var backtickToken = Current;
            Advance();
            if (Current.Kind != NameTokenKind.NumericLiteral)
            {
                var arity = CreateMissingToken(NameTokenKind.NumericLiteral);
                var result = new GenericNameNode(name.IdentifierToken, backtickToken, arity);
                diagnostics.Add(new NameDiagnostic(result, "A numerical value of arity is missing."));
                return result;
            }
            else
            {
                var result = new GenericNameNode(name.IdentifierToken, backtickToken, Current);
                Advance();
                return result;
            }
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