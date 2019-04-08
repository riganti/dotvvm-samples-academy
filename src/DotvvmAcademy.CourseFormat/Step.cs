namespace DotvvmAcademy.CourseFormat
{
    public class Step
    {
        public Step(
            string path,
            string moniker,
            string variantMoniker,
            string lessonMoniker,
            string name,
            CodeTask codeTask,
            Archive archive,
            EmbeddedView embeddedView)
        {
            Path = path;
            Moniker = moniker;
            VariantMoniker = variantMoniker;
            LessonMoniker = lessonMoniker;
            Name = name;
            CodeTask = codeTask;
            Archive = archive;
            EmbeddedView = embeddedView;
        }

        public string Path { get; }

        public string Moniker { get; }

        public string VariantMoniker { get; }

        public string LessonMoniker { get; }

        public string Name { get; }

        public CodeTask CodeTask { get; }

        public Archive Archive { get; }

        public EmbeddedView EmbeddedView { get; }
    }
}