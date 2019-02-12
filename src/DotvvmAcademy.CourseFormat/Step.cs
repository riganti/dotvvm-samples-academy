namespace DotvvmAcademy.CourseFormat
{
    public class Step : Source
    {
        public Step(string lessonMoniker, string variantMoniker, string stepMoniker, string text, string name)
            : base($"/{lessonMoniker}/{variantMoniker}/{stepMoniker}")
        {
            LessonMoniker = lessonMoniker;
            VariantMoniker = variantMoniker;
            StepMoniker = stepMoniker;
            Text = text;
            Name = name;
        }

        public EmbeddedView EmbeddedView { get; set; }

        public string LessonMoniker { get; }

        public string Name { get; }

        public string SolutionArchivePath { get; set; }

        public string StepMoniker { get; }

        public string Text { get; }

        public string CodeTaskPath { get; set; }

        public string VariantMoniker { get; }

        public override long GetSize()
        {
            return Text?.Length + 1 ?? 1;
        }
    }
}