using Microsoft.Extensions.Caching.Memory;
using System;

namespace DotvvmAcademy.CourseFormat
{
    public class CourseCacheWrapper : IDisposable
    {
        private const long ThirtyTwoMegabytes = 2 << 25;

        public IMemoryCache Cache { get; } = new MemoryCache(new MemoryCacheOptions { SizeLimit = ThirtyTwoMegabytes });

        public void Dispose()
        {
            Cache.Dispose();
        }
    }
}