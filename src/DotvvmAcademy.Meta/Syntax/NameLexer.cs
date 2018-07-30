using System.Globalization;

namespace DotvvmAcademy.Meta.Syntax
{
    public class NameLexer
    {
        public const char InvalidCharacter = char.MaxValue;

        private int position;
        private int start;

        public NameLexer(string source)
        {
            Source = source;
        }

        public string Source { get; }

        public static bool IsIdentifierChar(char character)
        {
            var category = CharUnicodeInfo.GetUnicodeCategory(character);
            switch (category)
            {
                case UnicodeCategory.UppercaseLetter:
                case UnicodeCategory.LowercaseLetter:
                case UnicodeCategory.TitlecaseLetter:
                case UnicodeCategory.ModifierLetter:
                case UnicodeCategory.OtherLetter:
                case UnicodeCategory.LetterNumber:
                case UnicodeCategory.DecimalDigitNumber:
                    return true;
            }

            return false;
        }

        public virtual NameToken Lex()
        {
            start = position;
            var character = Peek();
            switch (character)
            {
                case InvalidCharacter:
                    return CreateToken(NameTokenKind.End);

                case '[':
                    Advance();
                    return CreateToken(NameTokenKind.OpenBracket);

                case ']':
                    Advance();
                    return CreateToken(NameTokenKind.CloseBracket);

                case '`':
                    Advance();
                    return CreateToken(NameTokenKind.Backtick);

                case '.':
                    Advance();
                    return CreateToken(NameTokenKind.Dot);

                case ',':
                    Advance();
                    return CreateToken(NameTokenKind.Comma);

                case '*':
                    Advance();
                    return CreateToken(NameTokenKind.Asterisk);

                case ':':
                    if (Peek(1) == ':')
                    {
                        Advance();
                        Advance();
                        return CreateToken(NameTokenKind.ColonColon);
                    }
                    break;

                case '0':
                case '1':
                case '2':
                case '3':
                case '4':
                case '5':
                case '6':
                case '7':
                case '8':
                case '9':
                    Advance();
                    return ScanNumericLiteral();
            }
            if (IsIdentifierChar(character))
            {
                Advance();
                return ScanIdentifier();
            }

            Advance();
            return CreateToken(NameTokenKind.Bad);
        }

        protected void Advance()
        {
            position++;
        }

        protected NameToken CreateToken(NameTokenKind kind)
        {
            return new NameToken(kind, Source, start, position);
        }

        protected char Peek(int delta = 0)
        {
            if (position + delta >= Source.Length)
            {
                return InvalidCharacter;
            }

            return Source[position + delta];
        }

        private NameToken ScanIdentifier()
        {
            while (IsIdentifierChar(Peek()))
            {
                Advance();
            }

            return CreateToken(NameTokenKind.Identifier);
        }

        private NameToken ScanNumericLiteral()
        {
            while (true)
            {
                switch (Peek())
                {
                    case '0':
                    case '1':
                    case '2':
                    case '3':
                    case '4':
                    case '5':
                    case '6':
                    case '7':
                    case '8':
                    case '9':
                        Advance();
                        break;
                }
                break;
            }
            return CreateToken(NameTokenKind.NumericLiteral);
        }
    }
}