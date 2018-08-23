namespace DotvvmAcademy.CourseFormat
{
    public class RenderedLesson : RenderedSource<Lesson>
    {
        public RenderedLesson(Lesson source, string html, string imageUrl, string name) : base(source)
        {
            Html = html;
            ImageUrl = imageUrl;
            Name = name;
        }

        public string Html { get; }

        public string ImageUrl { get; }

        public string Name { get; }

        public override long GetSize()
        {
            var result = 0;
            result += (Html?.Length).GetValueOrDefault();
            result += (ImageUrl?.Length).GetValueOrDefault();
            result += (Name?.Length).GetValueOrDefault();
            return result;
        }
    }
}