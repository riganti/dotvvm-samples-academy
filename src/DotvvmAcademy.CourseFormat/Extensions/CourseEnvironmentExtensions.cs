using System;
using System.IO;
using System.Threading.Tasks;

namespace DotvvmAcademy.CourseFormat
{
    public static class CourseEnvironmentExtensions
    {
        public static async Task<string> ContextRead(
            this ICourseEnvironment environment,
            string lessonMoniker,
            string variantMoniker,
            string path)
        {
            if (SourcePath.IsAbsolute(path))
            {
                return await environment.Read(path);
            }
            if (SourcePath.IsRelative(path))
            {
                throw new ArgumentException("Path must not be relative.", nameof(path));
            }
            var absolutePath = $"/{lessonMoniker}/{variantMoniker}/{path}";
            if (await environment.Exists(absolutePath))
            {
                return await environment.Read(absolutePath);
            }
            absolutePath = $"/{lessonMoniker}/{path}";
            return await environment.Read(absolutePath);
        }

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