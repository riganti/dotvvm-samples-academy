using Markdig;
using Microsoft.Extensions.Caching.Memory;
using System;

namespace DotvvmAcademy.CourseFormat
{
    public class StepRenderer
    {
        public const string RenderedStepPrefix = "RenderedStep:";

        private readonly MarkdownExtractor extractor;
        private readonly CourseCacheWrapper wrapper;

        public StepRenderer(CourseCacheWrapper wrapper, MarkdownExtractor extractor)
        {
            this.wrapper = wrapper;
            this.extractor = extractor;
        }

        public RenderedStep Render(Step step)
        {
            if (step == null)
            {
                throw new ArgumentNullException(nameof(step));
            }

            return wrapper.Cache.GetOrCreate($"{RenderedStepPrefix}{step.Path}", entry =>
            {
                var document = Markdown.Parse(step.Text);
                var name = extractor.ExtractName(document);
                var codeTaskPath = extractor.ExtractCodeTaskPath(document);
                var html = extractor.ExtractHtml(document);
                var renderedStep = new RenderedStep(step, html, codeTaskPath, name);
                entry.Value = renderedStep;
                entry.AddExpirationToken(step.EvictionToken);
                entry.SetSize(step?.GetSize() ?? 1);
                return renderedStep;
            });
        }
    }
}