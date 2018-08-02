using Microsoft.Extensions.Caching.Memory;
using System;
using System.IO;
using System.Threading.Tasks;

namespace DotvvmAcademy.CourseFormat
{
    public class CourseWorkspace : IDisposable
    {
        public const string CachePrefix = "CourseFormat://";
#if DEBUG
        private static readonly TimeSpan SourceLifetime = TimeSpan.FromSeconds(3);
#else
        private static readonly TimeSpan SourceLifetime = TimeSpan.FromDays(1);
#endif

        // approximately 32 MiB
        private readonly MemoryCache cache = new MemoryCache(new MemoryCacheOptions { SizeLimit = 2 << 25 });
        private readonly SourceLoader loader;
        private readonly CourseEnvironment environment;

        public CourseWorkspace(CourseEnvironment environment, SourceLoader loader)
        {
            this.environment = environment;
            this.loader = loader;
        }

        public DirectoryInfo Root { get; }

        public Task<Source> Load(string sourcePath)
        {
            if (string.IsNullOrWhiteSpace(sourcePath))
            {
                throw new ArgumentException("Passed path is invalid.", nameof(sourcePath));
            }

            return cache.GetOrCreateAsync(sourcePath, async entry =>
            {
                var source = await loader.Load(sourcePath);
                entry.Value = source;
                entry.SetAbsoluteExpiration(SourceLifetime);
                entry.SetSize(source?.GetSize() ?? 1);
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
            return Load<Lesson>($"/{SourceVisitor.ContentDirectory}/{variant}/{lesson}");
        }

        public Task<WorkspaceRoot> LoadRoot()
        {
            return Load<WorkspaceRoot>("/");
        }

        public Task<Step> LoadStep(string variant, string lesson, string step)
        {
            return Load<Step>($"/{SourceVisitor.ContentDirectory}/{variant}/{lesson}/{step}");
        }

        public Task<CourseVariant> LoadVariant(string variant)
        {
            return Load<CourseVariant>($"/{SourceVisitor.ContentDirectory}/{variant}");
        }

        public void Dispose()
        {
            cache.Dispose();
        }
    }
}