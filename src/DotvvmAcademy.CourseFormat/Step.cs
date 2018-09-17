namespace DotvvmAcademy.CourseFormat
{
    public class Step : Source
    {
        public Step(string path, string text, string name) : base(path)
        {
            Text = text;
            Name = name;
            Moniker = SourcePath.GetLastSegment(Path).ToString();
        }

        public string ValidationScriptPath { get; set; }

        public EmbeddedView EmbeddedView { get; set; }

        public string Moniker { get; }

        public string Name { get; }

        public string Text { get; }

        public string SolutionArchivePath { get; set; }

        public override long GetSize()
        {
            return Text?.Length + 1 ?? 1;
        }
    }
}