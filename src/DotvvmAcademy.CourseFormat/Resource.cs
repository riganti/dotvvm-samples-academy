namespace DotvvmAcademy.CourseFormat
{
    public class Resource : Source
    {
        public Resource(string path, string text) : base(path)
        {
            Text = text;
        }

        public string Text { get; }

        public override long GetSize()
        {
            return Text.Length;
        }
    }
}