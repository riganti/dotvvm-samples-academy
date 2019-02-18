using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Concurrent;
using System.Threading;

namespace DotvvmAcademy.CourseFormat
{
    public class CourseCache : IMemoryCache
    {
        public const string SourcePrefix = "Source:";
#if DEBUG
        public static readonly TimeSpan SourceExpiration = TimeSpan.FromMinutes(10);
#else
        public static readonly TimeSpan SourceExpiration = TimeSpan.FromDays(1);
#endif
        private ConcurrentDictionary<string, IChangeToken> evictionTokens = new ConcurrentDictionary<string, IChangeToken>();

        private IMemoryCache cache = new MemoryCache(new MemoryCacheOptions { SizeLimit = 2 << 25 });

        public ICacheEntry CreateEntry(object key)
        {
            return cache.CreateEntry(key);
        }

        public void AddSource(Source source)
        {
            using (var entry = CreateEntry($"{SourcePrefix}{source.Path}"))
            {
                var tokenSource = new CancellationTokenSource();
                entry.Value = source;
                entry.SetAbsoluteExpiration(SourceExpiration);
                entry.SetSize(source.GetSize());
                entry.RegisterPostEvictionCallback((k, v, r, s) =>
                {
                    var cts = (CancellationTokenSource)s;
                    cts.Cancel();
                }, tokenSource);
                var token = new CancellationChangeToken(tokenSource.Token);
                evictionTokens.AddOrUpdate(source.Path, token, (p, v) => token);
            }
        }

        public void Dispose()
        {
            cache.Dispose();
        }

        public IChangeToken GetSourceEvictionToken(Source source)
        {
            if (evictionTokens.TryGetValue(source.Path, out var token))
            {
                return token;
            }

            return null;
        }

        public void Remove(object key)
        {
            cache.Remove(key);
        }

        public bool TryGetValue(object key, out object value)
        {
            return cache.TryGetValue(key, out value);
        }
    }
}