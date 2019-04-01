namespace DotvvmAcademy.CourseFormat
{
    public class Step
    {
        public Step(
            string path,
            string moniker,
            string name,
            CodeTask codeTask,
            Archive archive,
            EmbeddedView embeddedView)
        {
            Path = path;
            Moniker = moniker;
            Name = name;
            CodeTask = codeTask;
            Archive = archive;
            EmbeddedView = embeddedView;
        }

        public string Path { get; }

        public string Moniker { get; }

        public string Name { get; }

        public CodeTask CodeTask { get; }

        public Archive Archive { get; }

        public EmbeddedView EmbeddedView { get; }
    }
}