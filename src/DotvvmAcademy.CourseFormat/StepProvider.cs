using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DotvvmAcademy.CourseFormat
{
    public class StepProvider : ISourceProvider<Step>
    {
        private readonly ICourseEnvironment environment;
        private readonly MarkdigRenderer renderer = new MarkdigRenderer();

        public StepProvider(ICourseEnvironment environment)
        {
            this.environment = environment;
        }

        public async Task<Step> Get(string path)
        {
            // extract data from the environment
            var segments = SourcePath.GetSegments(path).ToArray();
            if (segments.Length != 3)
            {
                throw new ArgumentException($"Source path '{path}' is not composed of 3 segments.");
            }
            var step = new Step(
                lessonMoniker: segments[0].ToString(),
                variantMoniker: segments[1].ToString(),
                stepMoniker: segments[2].ToString());

            // extract data from the step itself
            using (var stream = environment.OpenRead($"{path}.md"))
            using (var reader = new StreamReader(stream))
            {
                var fileText = await reader.ReadToEndAsync();
                (var text, var frontMatter) = await renderer.Render<StepFrontMatter>(fileText);
                if (frontMatter == null)
                {
                    throw new InvalidOperationException($"Step at {path} doesn't have a YAML Front Matter.");
                }
                step.Text = text;
                step.Name = frontMatter.Title;
                step.ArchivePath = await environment.Find(step, frontMatter.Solution);
                step.CodeTaskPath = await environment.Find(step, frontMatter.CodeTask?.Path);
                step.CorrectPath = await environment.Find(step, frontMatter.CodeTask?.Correct);
                step.DefaultPath = await environment.Find(step, frontMatter.CodeTask?.Default);
                step.CodeTaskDependencies = await environment.FindMany(step, frontMatter.CodeTask?.Dependencies);
                step.EmbeddedViewPath = await environment.Find(step, frontMatter.EmbeddedView?.Path);
                step.EmbeddedViewDependencies = await environment.FindMany(step, frontMatter.EmbeddedView?.Dependencies);
            }
            return step;
        }
    }
}