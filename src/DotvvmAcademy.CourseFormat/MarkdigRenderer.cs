using Markdig;
using Markdig.Extensions.Yaml;
using Markdig.Renderers;
using System.IO;
using System.Threading.Tasks;
using YamlDotNet.Serialization;

namespace DotvvmAcademy.CourseFormat
{
    public class MarkdigRenderer
    {
        private readonly MarkdownPipeline pipeline = new MarkdownPipelineBuilder()
            .UsePipeTables()
            .UseEmphasisExtras()
            .UseYamlFrontMatter()
            .UseFigures()
            .Build();

        public Task<string> Render(string source)
        {
            var document = Markdown.Parse(source, pipeline);
            using var writer = new StringWriter();
            var renderer = new HtmlRenderer(writer);
            pipeline.Setup(renderer);
            renderer.Render(document);
            return Task.FromResult(writer.ToString());
        }

        public Task<(string html, TFrontMatter frontMatter)> Render<TFrontMatter>(string source)
        {
            var document = Markdown.Parse(source, pipeline);
            TFrontMatter frontMatter = default;
            if (document.Count > 0 && document[0] is YamlFrontMatterBlock frontMatterBlock)
            {
                var deserializer = new DeserializerBuilder()
                    .Build();
                frontMatter = deserializer.Deserialize<TFrontMatter>(frontMatterBlock.Lines.ToString());
            }
            using var writer = new StringWriter();
            var renderer = new HtmlRenderer(writer);
            pipeline.Setup(renderer);
            renderer.Render(document);
            var html = writer.ToString();
            return Task.FromResult((html, frontMatter));
        }
    }
}
