using System;
using System.Collections.Generic;

namespace DotvvmAcademy.DAL.Base.Models
{
    public sealed class SampleIdentifier : IEquatable<SampleIdentifier>
    {
        public SampleIdentifier(string lessonId, string language, int stepIndex, string path)
        {
            LessonId = lessonId;
            Language = language;
            StepIndex = stepIndex;
            Path = path;
        }

        public SampleIdentifier(StepIdentifier stepIdentifier, string path)
            : this(stepIdentifier.LessonId, stepIdentifier.Language, stepIdentifier.StepIndex, path)
        {
        }

        public SampleIdentifier(LessonIdentifier lessonIdentifier, int stepIndex, string path)
            : this(lessonIdentifier.LessonId, lessonIdentifier.Language, stepIndex, path)
        {
        }

        public string Language { get; }

        public string LessonId { get; }

        public string Path { get; }

        public int StepIndex { get; }

        public override bool Equals(object obj)
        {
            if (obj is SampleIdentifier identifier)
            {
                return Equals(identifier);
            }

            return false;
        }

        public bool Equals(SampleIdentifier identifier)
        {
            return identifier != null &&
                LessonId.Equals(identifier.LessonId) &&
                Language.Equals(identifier.Language) &&
                StepIndex.Equals(identifier.StepIndex) &&
                Path.Equals(identifier.Path);
        }

        public override int GetHashCode()
        {
            var hashCode = -117907278;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Language);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(LessonId);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Path);
            hashCode = hashCode * -1521134295 + StepIndex.GetHashCode();
            return hashCode;
        }
    }
}