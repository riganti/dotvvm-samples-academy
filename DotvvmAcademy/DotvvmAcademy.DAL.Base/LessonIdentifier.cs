using System;
using System.Collections.Generic;

namespace DotvvmAcademy.DAL.Base
{
    public sealed class LessonIdentifier : IEquatable<LessonIdentifier>
    {
        public LessonIdentifier(int index, string language)
        {
            Index = index;
            Language = language;
        }

        public int Index { get; }

        public string Language { get; }

        public override bool Equals(object obj)
        {
            if (obj is LessonIdentifier i)
            {
                return Equals(i);
            }

            return false;
        }

        public bool Equals(LessonIdentifier identifier)
        {
            return identifier != null &&
                Language.Equals(identifier.Language) &&
                Index.Equals(identifier.Index);
        }

        public override int GetHashCode()
        {
            var hashCode = -1263054562;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Language);
            hashCode = hashCode * -1521134295 + Index.GetHashCode();
            return hashCode;
        }
    }
}