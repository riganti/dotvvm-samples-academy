namespace DotvvmAcademy.CourseFormat
{
    public class Step : Source
    {
        public Step(string path, string text, string codeTask) : base(path)
        {
            Text = text;
            CodeTask = codeTask;
            Moniker = SourcePath.GetLastSegment(Path);
        }

        public string CodeTask { get; }

        public string Moniker { get; }

        public string Text { get; }
    }
}