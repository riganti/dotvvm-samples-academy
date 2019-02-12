using System;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DotvvmAcademy.CourseFormat
{
    public class StepProvider : ISourceProvider<Step>
    {
        private readonly ICourseEnvironment environment;
        private readonly IMarkdownRenderer renderer;

        public StepProvider(ICourseEnvironment environment, IMarkdownRenderer renderer)
        {
            this.environment = environment;
            this.renderer = renderer;
        }

        public async Task<Step> Get(string path)
        {
            var segments = SourcePath.GetSegments(path).ToArray();
            if (segments.Length != 3)
            {
                throw new ArgumentException($"Source path '{path}' is not composed of 3 segments.");
            }
            using (var stream = environment.OpenRead($"{path}.md"))
            using (var reader = new StreamReader(stream))
            {
                var fileText = await reader.ReadToEndAsync();
                (var html, var frontMatter) = await renderer.Render<StepFrontMatter>(fileText);
                if (frontMatter == null)
                {
                    throw new NotSupportedException($"Step at {path} doesn't have a YAML Front Matter.");
                }

                var step = new Step(
                    lessonMoniker: segments[0].ToString(),
                    variantMoniker: segments[1].ToString(),
                    stepMoniker: segments[2].ToString(),
                    text: html,
                    name: frontMatter.Title)
                {
                    CodeTaskPath = frontMatter.CodeTask,
                    SolutionArchivePath = frontMatter.Solution
                };
                if (frontMatter.EmbeddedView != null)
                {
                    var dependencies = frontMatter.EmbeddedView.Dependencies?.ToImmutableArray() ?? ImmutableArray.Create<string>();
                    step.EmbeddedView = new EmbeddedView(frontMatter.EmbeddedView.Path, dependencies);
                }
                return step;
            }
        }
    }
}