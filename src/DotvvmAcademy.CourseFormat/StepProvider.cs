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
            var file = (await environment.GetFiles(path))
                .Single(f => f.EndsWith(".md"));
            using (var stream = environment.OpenRead($"{path}/{file}"))
            using (var reader = new StreamReader(stream))
            {
                var fileText = await reader.ReadToEndAsync();
                (var html, var frontMatter) = await renderer.Render<StepFrontMatter>(fileText);
                if (frontMatter == null)
                {
                    throw new NotSupportedException($"Step at {path} doesn't have a YAML Front Matter.");
                }

                var step = new Step(
                    path: path,
                    text: html,
                    name: frontMatter.Title)
                {
                    ValidationScriptPath = frontMatter.CodeTask,
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