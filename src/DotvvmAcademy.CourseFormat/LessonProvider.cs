using System;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DotvvmAcademy.CourseFormat
{
    public class LessonProvider : ISourceProvider<Lesson>
    {
        private readonly ICourseEnvironment environment;
        private readonly IMarkdownRenderer renderer;

        public LessonProvider(ICourseEnvironment environment, IMarkdownRenderer renderer)
        {
            this.environment = environment;
            this.renderer = renderer;
        }

        public async Task<Lesson> Get(string path)
        {
            var file = (await environment.GetFiles(path))
                .Single(f => f.EndsWith(".md"));
            using (var stream = environment.OpenRead($"{path}/{file}"))
            using (var reader = new StreamReader(stream))
            {
                var steps = (await environment.GetDirectories(path)).ToImmutableArray();
                var fileText = await reader.ReadToEndAsync();
                (var html, var frontMatter) = await renderer.Render<LessonFrontMatter>(fileText);
                if (frontMatter == null)
                {
                    throw new NotSupportedException($"Lesson at '{path}' doesn't have YAML Front Matter.");
                }

                return new Lesson(
                    path: path,
                    annotation: html,
                    steps: steps,
                    name: frontMatter.Title,
                    imageUrl: frontMatter.Image,
                    status: frontMatter.Status);
            }
        }
    }
}