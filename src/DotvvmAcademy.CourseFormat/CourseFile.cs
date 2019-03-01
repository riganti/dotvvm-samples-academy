namespace DotvvmAcademy.CourseFormat
{
    public class CourseFile : Source
    {
        public CourseFile(string path, string text) : base(path)
        {
            Text = text;
        }

        public string Text { get; }
    }
}