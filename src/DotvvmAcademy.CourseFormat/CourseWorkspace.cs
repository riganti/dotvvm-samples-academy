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
            if (string.IsNullOrWhiteSpace(sourcePath))
            {
                throw new ArgumentException("Source path must not be null or whitespace.", nameof(sourcePath));
            }
            if (cache.TryGetValue($"{CourseCache.SourcePrefix}{sourcePath}", out var existingSource))
            {
                return (TSource)existingSource;
            }
            var sourceProvider = provider.GetRequiredService<ISourceProvider<TSource>>();
            var newSource = await sourceProvider.Get(sourcePath);
            cache.AddSource(newSource);
            return newSource;
        }
    }
}