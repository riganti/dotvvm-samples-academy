using Microsoft.Extensions.Caching.Memory;
using System;
using System.Threading.Tasks;

namespace DotvvmAcademy.CourseFormat
{
    public class CourseWorkspace
    {
        public const string SourcePrefix = "Source:";
#if DEBUG
        private static readonly TimeSpan SourceLifetime = TimeSpan.FromSeconds(3);
#else
        private static readonly TimeSpan SourceLifetime = TimeSpan.FromDays(1);
#endif
        private readonly CourseEnvironment environment;
        private readonly SourceLoader loader;
        private readonly CourseCacheWrapper wrapper;

        public CourseWorkspace(CourseEnvironment environment, SourceLoader loader, CourseCacheWrapper wrapper)
        {
            this.environment = environment;
            this.loader = loader;
            this.wrapper = wrapper;
        }

        public Task<Source> Load(string sourcePath)
        {
            if (string.IsNullOrWhiteSpace(sourcePath))
            {
                throw new ArgumentException("Passed path is invalid.", nameof(sourcePath));
            }

            return wrapper.Cache.GetOrCreateAsync($"{SourcePrefix}{sourcePath}", async entry =>
            {
                var source = await loader.Load(sourcePath);
                entry.Value = source;
                entry.SetAbsoluteExpiration(SourceLifetime);
                entry.SetSize(source?.GetSize() ?? 1);
                entry.RegisterPostEvictionCallback((k, v, r, s) => source?.OnEviction());
                return source;
            });
        }

        public async Task<TSource> Load<TSource>(string sourcePath)
            where TSource : Source
        {
            var source = await Load(sourcePath);
            if (source == null)
            {
                return null;
            }

            if (source is TSource cast)
            {
                return cast;
            }
            else
            {
                throw new ArgumentException($"Path doesn't point to a '{typeof(TSource)}'.", nameof(sourcePath));
            }
        }

        public Task<Lesson> LoadLesson(string variant, string lesson)
        {
            return Load<Lesson>($"/{CourseEnvironment.ContentDirectory}/{variant}/{lesson}");
        }

        public Task<WorkspaceRoot> LoadRoot()
        {
            return Load<WorkspaceRoot>("/");
        }

        public Task<Step> LoadStep(string variant, string lesson, string step)
        {
            return Load<Step>($"/{CourseEnvironment.ContentDirectory}/{variant}/{lesson}/{step}");
        }

        public Task<CourseVariant> LoadVariant(string variant)
        {
            return Load<CourseVariant>($"/{CourseEnvironment.ContentDirectory}/{variant}");
        }
    }
}