using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace DotvvmAcademy.CourseFormat
{
    public class CourseWorkspace
    {
        private readonly CourseCache cache;
        private readonly IServiceProvider provider;

        public CourseWorkspace(CourseCache cache, IServiceProvider provider)
        {
            this.cache = cache;
            this.provider = provider;
        }

        public async Task<TSource> Load<TSource>(string sourcePath)
            where TSource : Source
        {
            if (!SourcePath.IsAbsolute(sourcePath))
            {
                throw new ArgumentException("Source path must be absolute.", nameof(sourcePath));
            }
            if (cache.TryGetValue($"{CourseCache.SourcePrefix}{sourcePath}", out var existingSource))
            {
                return (TSource)existingSource;
            }
            var sourceProvider = provider.GetRequiredService<ISourceProvider<TSource>>();
            var newSource = await sourceProvider.Get(sourcePath);
            if (newSource == null)
            {
                return null;
            }

            cache.AddSource(newSource);
            return newSource;
        }

        public async Task<TSource> ContextLoad<TSource>(string lessonMoniker, string variantMoniker, string sourcePath)
            where TSource : Source
        {
            if (SourcePath.IsAbsolute(sourcePath))
            {
                return await Load<TSource>(sourcePath);
            }
            else if (SourcePath.IsRelative(sourcePath))
            {
                throw new ArgumentException("Source path must not be relative.", nameof(sourcePath));
            }
            else
            {
                var absolutePath = $"/{lessonMoniker}/{variantMoniker}/{sourcePath}";
                var source = await Load<TSource>(absolutePath);
                if(source != null)
                {
                    return source;
                }
                absolutePath = $"/{lessonMoniker}/{sourcePath}";
                return await Load<TSource>(absolutePath);
            }
        }
    }
}