namespace DotvvmAcademy.CourseFormat
{
    public class Step : Source
    {
        public Step(string path, string text, string name, string codeTaskPath) : base(path)
        {
            Text = text;
            Name = name;
            CodeTaskPath = codeTaskPath;
            Moniker = SourcePath.GetLastSegment(Path).ToString();
        }

        public string CodeTaskPath { get; }

        public string Moniker { get; }

        public string Name { get; }

        public string Text { get; }

        public override long GetSize()
        {
            return Text?.Length + 1 ?? 1;
        }
    }
}