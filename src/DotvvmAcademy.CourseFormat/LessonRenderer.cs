using Markdig;
using Microsoft.Extensions.Caching.Memory;
using System;

namespace DotvvmAcademy.CourseFormat
{
    public class LessonRenderer
    {
        public const string RenderedLessonPrefix = "RenderedLesson:";

        private readonly MarkdownExtractor extractor;
        private readonly CourseCacheWrapper wrapper;

        public LessonRenderer(CourseCacheWrapper wrapper, MarkdownExtractor extractor)
        {
            this.wrapper = wrapper;
            this.extractor = extractor;
        }

        public RenderedLesson Render(Lesson lesson)
        {
            if (lesson == null)
            {
                throw new ArgumentNullException(nameof(lesson));
            }

            return wrapper.Cache.GetOrCreate($"{RenderedLessonPrefix}{lesson.Path}", entry =>
            {
                var document = Markdown.Parse(lesson.Annotation);
                var name = extractor.ExtractName(document);
                var imageUrl = extractor.ExtractImageUrl(document);
                var html = extractor.ExtractHtml(document);
                var renderedLesson = new RenderedLesson(lesson, html, imageUrl, name);
                entry.Value = renderedLesson;
                entry.AddExpirationToken(lesson.EvictionToken);
                entry.SetSize(renderedLesson?.GetSize() ?? 1);
                return renderedLesson;
            });
        }
    }
}