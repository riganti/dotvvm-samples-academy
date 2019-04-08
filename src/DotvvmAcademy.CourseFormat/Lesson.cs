using System.Collections.Generic;
using System.Collections.Immutable;

namespace DotvvmAcademy.CourseFormat
{
    public class Lesson
    {
        private readonly ImmutableDictionary<string, LessonVariant> associativeVariants;

        public Lesson(string path, string moniker, IEnumerable<LessonVariant> variants)
        {
            Path = path;
            Moniker = moniker;
            Variants = variants.ToImmutableArray();
            associativeVariants = variants.ToImmutableDictionary(v => v.Moniker);
        }

        public string Path { get; }

        public string Moniker { get; }

        public ImmutableArray<LessonVariant> Variants { get; }

        public LessonVariant GetVariant(string moniker)
        {
            if(associativeVariants.TryGetValue(moniker, out var variant))
            {
                return variant;
            }
            return null;
        }
    }
}