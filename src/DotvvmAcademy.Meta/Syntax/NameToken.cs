using System.Diagnostics;
using System.Text;

namespace DotvvmAcademy.Meta.Syntax
{
    [DebuggerDisplay("{GetDebuggerDisplay(), nq}")]
    public struct NameToken
    {
        public NameToken(NameTokenKind kind, string source, int start, int end, bool isMissing = false)
        {
            Kind = kind;
            Source = source;
            Start = start;
            End = end;
            IsMissing = isMissing;
        }

        public bool IsMissing { get; }

        public int End { get; }

        public NameTokenKind Kind { get; }

        public string Source { get; }

        public int Start { get; }

        public override string ToString()
        {
            return Source.Substring(Start, End - Start);
        }

        private string GetDebuggerDisplay()
        {
            var stringBuilder = new StringBuilder()
                .Append(Kind.ToString())
                .Append(" [")
                .Append(Start)
                .Append(", ")
                .Append(End)
                .Append(')');
            if (IsMissing)
            {
                stringBuilder
                    .Append(" [Missing]");
            }
            else
            {
                stringBuilder
                    .Append(' ')
                    .Append(ToString());
            }

            return stringBuilder.ToString();
        }
    }
}