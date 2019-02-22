using System.Collections.Immutable;

namespace DotvvmAcademy.CourseFormat
{
    public class Step : Source
    {
        public Step(string lessonMoniker, string variantMoniker, string stepMoniker)
            : base($"/{lessonMoniker}/{variantMoniker}/{stepMoniker}")
        {
            LessonMoniker = lessonMoniker;
            VariantMoniker = variantMoniker;
            StepMoniker = stepMoniker;
        }

        public string LessonMoniker { get; }

        public string VariantMoniker { get; }

        public string StepMoniker { get; }

        public string Text { get; set; }

        public string ArchivePath { get; internal set; }

        public string Name { get; internal set; }

        public string CodeTaskPath { get; internal set; }

        public string CorrectPath { get; internal set; }

        public string DefaultPath { get; internal set; }

        public ImmutableArray<string> CodeTaskDependencies { get; internal set; }

        public string EmbeddedViewPath { get; internal set; }

        public ImmutableArray<string> EmbeddedViewDependencies { get; internal set; }

        public override long GetSize()
        {
            return Text?.Length + 1 ?? 1;
        }
    }
}