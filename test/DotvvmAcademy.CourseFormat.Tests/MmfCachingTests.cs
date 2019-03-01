using Microsoft.Extensions.Caching.Memory;
using System;
using System.IO.MemoryMappedFiles;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace DotvvmAcademy.CourseFormat.Tests
{
    public class MmfCachingTests
    {
        [Fact(Skip = "Not working")]
        public void MemoryCache_WithMmf_NoRaceCondition()
        {
            const string MapName = "CourseFormat/TestFile";
            const string MutexName = "CourseFormat/TestMutex";
            const string SourceKey = "TestSource";

            var cache = new MemoryCache(new MemoryCacheOptions());
            TestSource GetSource()
            {
                if (cache.TryGetValue<TestSource>(SourceKey, out var source))
                {
                    return source;
                }
                var mutex = new Mutex(true, MutexName, out var isMutexNew);
                mutex.WaitOne();
                if (!isMutexNew)
                {
                    // there might a waiting Dispose call
                    mutex.ReleaseMutex(); 
                    mutex.WaitOne();
                }
                var file = MemoryMappedFile.CreateNew(MapName, 4096, MemoryMappedFileAccess.ReadWrite);
                source = new TestSource(SourceKey, file, mutex);
                using (var entry = cache.CreateEntry(SourceKey))
                {
                    entry.Value = source;
                    entry.SetAbsoluteExpiration(TimeSpan.FromMilliseconds(500));
                    entry.RegisterPostEvictionCallback((key, value, reason, state) =>
                    {
                        if(value is IDisposable disposable)
                        {
                            disposable.Dispose();
                        }
                    });
                }
                mutex.ReleaseMutex();
                return source;
            }
            GetSource();
            Thread.Sleep(1000);
            GetSource();
        }

        private class TestSource : Source, IDisposable
        {
            public TestSource(string path, MemoryMappedFile file, Mutex mutex) : base(path)
            {
                File = file;
                Mutex = mutex;
            }

            public MemoryMappedFile File { get; }

            public Mutex Mutex { get; }

            public void Dispose()
            {
                Mutex.WaitOne();
                File.Dispose();
                Mutex.ReleaseMutex();
                Mutex.Dispose();
            }
        }
    }
}