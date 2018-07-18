namespace DotvvmAcademy.CourseFormat
{
    public class MarkdownLessonInfo
    {
        internal MarkdownLessonInfo(ILesson lesson, string html, string imageUrl, string name)
        {
            Lesson = lesson;
            Html = html;
            ImageUrl = imageUrl;
            Name = name;
        }

        public string Html { get; }

        public string ImageUrl { get; }

        public ILesson Lesson { get; }

        public string Name { get; }
    }
}