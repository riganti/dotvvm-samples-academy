using Markdig;
using Markdig.Extensions.Yaml;
using Markdig.Renderers;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.IO;
using YamlDotNet.Serialization;

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
                    .UsePipeTables()
                    .UseEmphasisExtras()
                    .UseYamlFrontMatter()
                    .Build();
                var document = Markdown.Parse(step.Text ?? string.Empty, pipeline);
                StepFrontMatter frontMatter;
                if (document.Count > 0 && document[0] is YamlFrontMatterBlock frontMatterBlock)
                {
                    var deserializer = new DeserializerBuilder()
                        .Build();
                    frontMatter = deserializer.Deserialize<StepFrontMatter>(frontMatterBlock.Lines.ToString());
                }
                else
                {
                    frontMatter = new StepFrontMatter();
                }
                var html = string.Empty;
                using (var writer = new StringWriter())
                {
                    var renderer = new HtmlRenderer(writer);
                    pipeline.Setup(renderer);
                    renderer.Render(document);
                    html = writer.ToString();
                }
                var renderedStep = new RenderedStep(step, html, frontMatter.CodeTask, frontMatter.Title);
                entry.Value = renderedStep;
                entry.AddExpirationToken(step.EvictionToken);
                entry.SetSize(step.GetSize());
                return renderedStep;
            });
        }
    }
}