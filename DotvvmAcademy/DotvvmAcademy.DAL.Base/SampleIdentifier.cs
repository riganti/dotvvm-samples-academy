using System;
using System.Collections.Generic;

namespace DotvvmAcademy.DAL.Base
{
    public sealed class SampleIdentifier : IEquatable<SampleIdentifier>
    {
        public SampleIdentifier(int lessonIndex, string language, int stepIndex, string path)
        {
            LessonIndex = lessonIndex;
            Language = language;
            StepIndex = stepIndex;
            Path = path;
        }

        public string Language { get; }

        public int LessonIndex { get; }

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
                LessonIndex == identifier.LessonIndex &&
                Language == identifier.Language &&
                StepIndex == identifier.StepIndex &&
                Path == identifier.Path;
        }

        public override int GetHashCode()
        {
            var hashCode = 447180285;
            hashCode = hashCode * -1521134295 + LessonIndex.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Language);
            hashCode = hashCode * -1521134295 + StepIndex.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Path);
            return hashCode;
        }
    }
}