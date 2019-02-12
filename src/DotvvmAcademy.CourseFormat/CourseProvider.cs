using System;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;

namespace DotvvmAcademy.CourseFormat
{
    public class CourseProvider : ISourceProvider<Course>
    {
        private readonly ICourseEnvironment environment;

        public CourseProvider(ICourseEnvironment environment)
        {
            this.environment = environment;
        }

        public async Task<Course> Get(string path)
        {
            var lessons = ImmutableArray.CreateBuilder<string>();
            foreach(var directory in await environment.GetDirectories("/"))
            {
                if (directory.StartsWith("."))
                {
                    continue;
                }
                var isLesson = false;
                foreach(var subdirectory in await environment.GetDirectories($"/{directory}"))
                {
                    var files = await environment.GetFiles($"/{directory}/{subdirectory}");
                    if (files.Contains(CourseConstants.LessonFile))
                    {
                        isLesson = true;
                        break;
                    }
                }
                if (isLesson)
                {
                    lessons.Add(directory);
                }
            }
            return new Course(lessons.ToImmutable());
        }
    }
}