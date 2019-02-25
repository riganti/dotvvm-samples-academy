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
        private readonly MarkdigRenderer renderer = new MarkdigRenderer();

        public LessonVariantProvider(ICourseEnvironment environment)
        {
            this.environment = environment;
        }

        public async Task<LessonVariant> Get(string path)
        {
            // extract mandatory data from the environment
            var segments = SourcePath.GetSegments(path).ToArray();
            if (segments.Length != 2)
            {
                throw new ArgumentException($"Source path '{path}' is not composed of 2 segments.");
            }
            var steps = (await environment.GetFiles(path))
                .Where(f => f.EndsWith(".md") && f != CourseConstants.LessonFile)
                .Select(f => f.Substring(0, f.Length - 3))
                .ToImmutableArray();
            if (steps.Length == 0)
            {
                throw new NotSupportedException($"Lesson at '{path}' contains no steps.");
            }
            var variant = new LessonVariant(
                lessonMoniker: segments[0].ToString(),
                variantMoniker: segments[1].ToString(),
                steps: steps);

            // extract data from lesson.md
            using (var stream = environment.OpenRead($"{path}/{CourseConstants.LessonFile}"))
            using (var reader = new StreamReader(stream))
            {
                var fileText = await reader.ReadToEndAsync();
                (var annotation, var frontMatter) = await renderer.Render<LessonFrontMatter>(fileText);
                if (frontMatter == null)
                {
                    throw new InvalidOperationException($"LessonVariant at {path} doesn't have a YAML Front Matter.");
                }
                variant.Annotation = annotation;
                variant.ImageUrl = frontMatter.Image;
                variant.Name = frontMatter.Title;
                variant.Status = frontMatter.Status;
            }
            return variant;
        }
    }
}