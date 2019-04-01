using System.Collections.Immutable;

namespace DotvvmAcademy.CourseFormat
{
    public class Lesson
    {
        public Lesson(string path, string moniker, ImmutableDictionary<string, LessonVariant> variants)
        {
            Path = path;
            Moniker = moniker;
            Variants = variants;
        }

        public string Path { get; }

        public string Moniker { get; }

        public ImmutableDictionary<string, LessonVariant> Variants { get; }
    }
}