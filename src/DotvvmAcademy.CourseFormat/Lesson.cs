using System.Collections.Immutable;

namespace DotvvmAcademy.CourseFormat
{
    public class Lesson : Source
    {
        public Lesson(string moniker, ImmutableArray<string> variants) : base($"/{moniker}")
        {
            Moniker = moniker;
            Variants = variants;
        }

        public string Moniker { get; }

        public ImmutableArray<string> Variants { get; }
    }
}