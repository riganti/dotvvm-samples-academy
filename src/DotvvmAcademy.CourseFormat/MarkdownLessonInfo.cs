namespace DotvvmAcademy.CourseFormat
{
    public class MarkdownLessonInfo
    {
        public MarkdownLessonInfo(Lesson lesson, string html, string imageUrl, string name)
        {
            Lesson = lesson;
            Html = html;
            ImageUrl = imageUrl;
            Name = name;
        }

        public string Html { get; }

        public string ImageUrl { get; }

        public Lesson Lesson { get; }

        public string Name { get; }
    }
}