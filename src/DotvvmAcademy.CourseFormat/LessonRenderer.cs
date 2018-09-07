using Markdig;
using Markdig.Renderers;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.IO;

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
                var pipeline = new MarkdownPipelineBuilder()
                    .UsePipeTables()
                    .UseEmphasisExtras()
                    .Build();
                var document = Markdown.Parse(lesson.Annotation ?? string.Empty, pipeline);
                var name = extractor.ExtractName(document);
                var imageUrl = extractor.ExtractImageUrl(document);
                using (var writer = new StringWriter())
                {
                    var renderer = new HtmlRenderer(writer);
                    pipeline.Setup(renderer);
                    renderer.Render(document);
                    var html = writer.ToString();
                    var renderedLesson = new RenderedLesson(lesson, html, imageUrl, name);
                    entry.Value = renderedLesson;
                    entry.AddExpirationToken(lesson.EvictionToken);
                    entry.SetSize(renderedLesson.GetSize());
                    return renderedLesson;
                }
            });
        }
    }
}