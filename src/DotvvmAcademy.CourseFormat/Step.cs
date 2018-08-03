namespace DotvvmAcademy.CourseFormat
{
    public class Step : Source
    {
        public Step(string path, string text) : base(path)
        {
            Text = text;
            Moniker = SourcePath.GetLastSegment(Path).ToString();
        }

        public string Moniker { get; }

        public string Text { get; }

        public override long GetSize()
        {
            return Text.Length;
        }
    }
}