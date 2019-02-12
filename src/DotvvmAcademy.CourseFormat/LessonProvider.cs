using System;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;

namespace DotvvmAcademy.CourseFormat
{
    public class LessonProvider : ISourceProvider<Lesson>
    {
        private readonly ICourseEnvironment environment;

        public LessonProvider(ICourseEnvironment environment)
        {
            this.environment = environment;
        }

        public async Task<Lesson> Get(string path)
        {
            var segments = SourcePath.GetSegments(path).ToArray();
            if (segments.Length != 1)
            {
                throw new ArgumentException($"Source path '{path}' is not composed of a single segment.");
            }
            if (!await environment.Exists(path))
            {
                return null;
            }
            var variants = ImmutableArray.CreateBuilder<string>();
            foreach (var directory in await environment.GetDirectories(path))
            {
                var files = await environment.GetFiles($"{path}/{directory}");
                if (files.Contains(CourseConstants.LessonFile))
                {
                    variants.Add(directory);
                }
            }
            return new Lesson(
                moniker: segments[0].ToString(),
                variants: variants.ToImmutable());
        }
    }
}