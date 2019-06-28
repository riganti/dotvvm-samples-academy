using System.Collections.Generic;
using System.Collections.Immutable;

namespace DotvvmAcademy.CourseFormat
{
    public class LessonVariant
    {
        private readonly ImmutableDictionary<string, Step> associativeSteps;

        public LessonVariant(
            string path,
            string moniker,
            string lessonMoniker,
            string annotationPath,
            string imageUrl,
            string name,
            LessonStatus status,
            IEnumerable<Step> steps)
        {
            Path = path;
            Moniker = moniker;
            LessonMoniker = lessonMoniker;
            AnnotationPath = annotationPath;
            ImageUrl = imageUrl;
            Name = name;
            Status = status;
            Steps = steps.ToImmutableArray();
            associativeSteps = steps.ToImmutableDictionary(s => s.Moniker);
        }

        public string Path { get; }

        public string Moniker { get; }

        public string LessonMoniker { get; }

        public string AnnotationPath { get; }

        public string ImageUrl { get; }

        public string Name { get; }

        public LessonStatus Status { get; }

        public ImmutableArray<Step> Steps { get; }

        public Step GetStep(string moniker)
        {
            if (associativeSteps.TryGetValue(moniker, out var step))
            {
                return step;
            }
            return null;
        }
    }
}