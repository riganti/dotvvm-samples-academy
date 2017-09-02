using System;
using System.Collections.Generic;

namespace DotvvmAcademy.DAL.Base.Models
{
    public sealed class StepIdentifier : IEquatable<StepIdentifier>
    {
        public StepIdentifier(string lessonId, string language, int stepIndex)
        {
            LessonId = lessonId;
            Language = language;
            StepIndex = stepIndex;
        }

        public StepIdentifier(LessonIdentifier lessonIdentifier, int stepIndex)
            : this(lessonIdentifier.LessonId, lessonIdentifier.Language, stepIndex)
        {
        }

        public string Language { get; }

        public string LessonId { get; }

        public int StepIndex { get; }

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
                LessonId.Equals(identifier.LessonId) &&
                StepIndex.Equals(identifier.StepIndex);
        }

        public override int GetHashCode()
        {
            var hashCode = 622116884;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Language);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(LessonId);
            hashCode = hashCode * -1521134295 + StepIndex.GetHashCode();
            return hashCode;
        }
    }
}