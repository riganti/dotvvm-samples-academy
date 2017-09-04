using System;
using System.Collections.Generic;

namespace DotvvmAcademy.DAL.Base.Models
{
    public sealed class LessonIdentifier : IEquatable<LessonIdentifier>
    {
        public LessonIdentifier(string lessonId, string language)
        {
            LessonId = lessonId;
            Language = language;
        }

        public string Language { get; }

        public string LessonId { get; }

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
                LessonId.Equals(identifier.LessonId);
        }

        public override int GetHashCode()
        {
            var hashCode = -1781179249;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(LessonId);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Language);
            return hashCode;
        }
    }
}