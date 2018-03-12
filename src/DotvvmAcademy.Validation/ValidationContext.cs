using System;
using System.Collections.Concurrent;

namespace DotvvmAcademy.Validation
{
    public class ValidationContext
    {
        public const string DiagnosticsKey = "Diagnostics";

        private ConcurrentDictionary<string, object> items 
            = new ConcurrentDictionary<string, object>();

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