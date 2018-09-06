using Markdig;
using Markdig.Extensions.Tables;
using Markdig.Renderers;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.IO;

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
                var pipeline = new MarkdownPipelineBuilder()
                    .UseAdvancedExtensions()
                    .Build();
                var document = Markdown.Parse(step.Text, pipeline);
                var name = extractor.ExtractName(document);
                var codeTaskPath = extractor.ExtractCodeTaskPath(document);
                using (var writer = new StringWriter())
                {
                    var renderer = new HtmlRenderer(writer);
                    pipeline.Setup(renderer);
                    renderer.Render(document);
                    var html = writer.ToString();
                    var renderedStep = new RenderedStep(step, html, codeTaskPath, name);
                    entry.Value = renderedStep;
                    entry.AddExpirationToken(step.EvictionToken);
                    entry.SetSize(step.GetSize());
                    return renderedStep;
                }
            });
        }
    }
}