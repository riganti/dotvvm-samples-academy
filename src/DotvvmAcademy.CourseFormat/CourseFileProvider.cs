using System.Threading.Tasks;

namespace DotvvmAcademy.CourseFormat
{
    public class CourseFileProvider : ISourceProvider<CourseFile>
    {
        private readonly ICourseEnvironment environment;

        public CourseFileProvider(ICourseEnvironment environment)
        {
            this.environment = environment;
        }

        public async Task<CourseFile> Get(string path)
        {
            return new CourseFile(path, await environment.Read(path));
        }
    }
}