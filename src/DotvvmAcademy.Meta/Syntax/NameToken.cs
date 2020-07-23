using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace DotvvmAcademy.Meta.Syntax
{
    [DebuggerDisplay("{GetDebuggerDisplay(), nq}")]
    public struct NameToken : IEquatable<NameToken>
    {
        public NameToken(NameTokenKind kind, string source, int start, int end, bool isMissing = false)
        {
            Kind = kind;
            Source = source;
            Start = start;
            End = end;
            IsMissing = isMissing;
        }

        public int End { get; }

        public bool IsMissing { get; }

        public NameTokenKind Kind { get; }

        public string Source { get; }

        public int Start { get; }

        public static bool operator !=(NameToken token1, NameToken token2)
        {
            return !(token1 == token2);
        }

        public static bool operator ==(NameToken token1, NameToken token2)
        {
            return token1.Equals(token2);
        }

        public override bool Equals(object obj)
        {
            return obj is NameToken token && Equals(token);
        }

        public bool Equals(NameToken other)
        {
            return IsMissing == other.IsMissing &&
                   End == other.End &&
                   Kind == other.Kind &&
                   Source == other.Source &&
                   Start == other.Start;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(IsMissing, End, Kind, Source, Start);
        }

        public override string ToString()
        {
            return Source[Start..End];
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
