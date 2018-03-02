using System.Collections.Concurrent;

namespace DotvvmAcademy.Validation
{
    public class ValidationContext
    {
        private ConcurrentDictionary<string, object> items;

        public object GetItem(string key)
        {
            if (items.TryGetValue(key, out var value))
            {
                return value;
            }

            return null;
        }

        public void SetItem(string key, object item)
        {
            items.AddOrUpdate(key, item, (k, o) => item);
        }
    }
}