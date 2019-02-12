using System;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DotvvmAcademy.CourseFormat
{
    public class LessonVariantProvider : ISourceProvider<LessonVariant>
    {
        private readonly ICourseEnvironment environment;
        private readonly IMarkdownRenderer renderer;

        public LessonVariantProvider(ICourseEnvironment environment, IMarkdownRenderer renderer)
        {
            this.environment = environment;
            this.renderer = renderer;
        }

        public async Task<LessonVariant> Get(string path)
        {
            // extract the monikers
            var segments = SourcePath.GetSegments(path).ToArray();
            if (segments.Length != 2)
            {
                throw new ArgumentException($"Source path '{path}' is not composed of 2 segments.");
            }
            var lessonMoniker = segments[0].ToString();
            var variantMoniker = segments[1].ToString();

            using (var stream = environment.OpenRead($"{path}/{CourseConstants.LessonFile}"))
            using (var reader = new StreamReader(stream))
            {
                var fileText = await reader.ReadToEndAsync();
                (var html, var frontMatter) = await renderer.Render<LessonFrontMatter>(fileText);
                if (frontMatter == null)
                {
                    throw new NotSupportedException($"Lesson at '{path}' doesn't have a YAML Front Matter.");
                }

                var steps = (await environment.GetFiles(path))
                    .Where(f => f.EndsWith(".md") && f != CourseConstants.LessonFile)
                    .Select(f => f.Substring(0, f.Length - 3))
                    .ToImmutableArray();
                if (steps.Length == 0)
                {
                    throw new NotSupportedException($"Lesson at '{path}' contains no steps.");
                }

                return new LessonVariant(
                    lessonMoniker: lessonMoniker,
                    variantMoniker: variantMoniker,
                    steps: steps,
                    name: frontMatter.Title,
                    status: frontMatter.Status,
                    imageUrl: frontMatter.Image,
                    annotation: html);
            }
        }
    }
}