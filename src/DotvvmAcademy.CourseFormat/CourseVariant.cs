using System;
using System.Collections.Immutable;

namespace DotvvmAcademy.CourseFormat
{
    public class CourseVariant : Source
    {
        public CourseVariant(string path, string annotation, ImmutableArray<string> lessons) : base(path)
        {
            Annotation = annotation;
            Lessons = lessons;
            Moniker = SourcePath.GetLastSegment(Path).ToString();
        }

        public string Annotation { get; }

        public ImmutableArray<string> Lessons { get; }

        public string Moniker { get; }

        public override long GetSize()
        {
            return Annotation?.Length ?? 1;
        }
    }
}