using System.IO;
using System.Threading.Tasks;

namespace DotvvmAcademy.CourseFormat
{
    public static class CourseEnvironmentExtensions
    {
        public static async Task<string> Read(this ICourseEnvironment environment, string path)
        {
            using (var stream = environment.OpenRead(path))
            using (var reader = new StreamReader(stream))
            {
                return await reader.ReadToEndAsync();
            }
        }
    }
}