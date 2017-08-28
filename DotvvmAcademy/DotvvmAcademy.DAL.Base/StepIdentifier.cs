using System;
using System.Collections.Generic;

namespace DotvvmAcademy.DAL.Base
{
    public sealed class StepIdentifier : IEquatable<StepIdentifier>
    {
        public StepIdentifier(int lessonIndex, string language, int index)
        {
            LessonIndex = lessonIndex;
            Language = language;
            Index = index;
        }

        public int Index { get; }

        public string Language { get; }

        public int LessonIndex { get; }

        public override bool Equals(object obj)
        {
            if (obj is StepIdentifier i)
            {
                return Equals(i);
            }

            return false;
        }

        public bool Equals(StepIdentifier identifier)
        {
            return identifier != null &&
                Language.Equals(identifier.Language) &&
                LessonIndex.Equals(identifier.LessonIndex) &&
                Index.Equals(identifier.Index);
        }

        public override int GetHashCode()
        {
            var hashCode = -142102651;
            hashCode = hashCode * -1521134295 + Index.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Language);
            hashCode = hashCode * -1521134295 + LessonIndex.GetHashCode();
            return hashCode;
        }
    }
}