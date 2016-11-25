using System.Collections.Concurrent;

namespace DotvvmAcademy.Cache
{
    public static class Cache
    {
        private static ConcurrentDictionary<string, object> dictionary { get; set; } = new ConcurrentDictionary<string, object>();

        public static void Set<TKey, TValue>(TValue value, string key = null)
        {
            dictionary.TryAdd(GetCacheKey<TKey, TValue>(key), value);
        }

        private static string GetCacheKey<TKey, TValue>(string key = null)
        {
            return key ?? typeof(TKey).FullName + "|" + typeof(TValue).FullName;
        }

        public static TValue Get<TKey, TValue>(string key)
        {
            object value;
            dictionary.TryGetValue(GetCacheKey<TKey, TValue>(), out value);
            return (TValue)value;
        }
    }
}
