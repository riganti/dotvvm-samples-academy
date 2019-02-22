using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DotvvmAcademy.CourseFormat
{
    public static class CourseEnvironmentExtensions
    {
        public static async Task<string> Find(
            this ICourseEnvironment environment,
            string lessonMoniker,
            string variantMoniker,
            string path)
        {
            if (path == null)
            {
                return null;
            }
            if (SourcePath.IsAbsolute(path))
            {
                return path;
            }
            if (SourcePath.IsRelative(path))
            {
                throw new ArgumentException("Path must not be relative.", nameof(path));
            }
            var absolutePath = $"/{lessonMoniker}/{variantMoniker}/{path}";
            if (await environment.Exists(absolutePath))
            {
                return absolutePath;
            }
            absolutePath = $"/{lessonMoniker}/{path}";
            if (await environment.Exists(absolutePath))
            {
                return absolutePath;
            }
            throw new InvalidOperationException($"No file or directory could be found at '{path}'.");
        }

        public static Task<string> Find(this ICourseEnvironment environment, LessonVariant context, string path)
        {
            return environment.Find(context.LessonMoniker, context.VariantMoniker, path);
        }

        public static Task<string> Find(this ICourseEnvironment environment, Step context, string path)
        {
            return environment.Find(context.LessonMoniker, context.VariantMoniker, path);
        }

        public static async Task<ImmutableArray<string>> FindMany(
            this ICourseEnvironment environment,
            string lessonMoniker,
            string variantMoniker,
            IEnumerable<string> paths)
        {
            if (paths == null)
            {
                return ImmutableArray.Create<string>();
            }
            return (await Task.WhenAll(paths.Select(p => environment.Find(lessonMoniker, variantMoniker, p))))
                .ToImmutableArray();
        }

        public static Task<ImmutableArray<string>> FindMany(
            this ICourseEnvironment environment,
            LessonVariant context,
            IEnumerable<string> path)
        {
            return environment.FindMany(context.LessonMoniker, context.VariantMoniker, path);
        }

        public static Task<ImmutableArray<string>> FindMany(
            this ICourseEnvironment environment,
            Step context,
            IEnumerable<string> path)
        {
            return environment.FindMany(context.LessonMoniker, context.VariantMoniker, path);
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