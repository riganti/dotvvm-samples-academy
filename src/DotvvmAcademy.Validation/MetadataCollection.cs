using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace DotvvmAcademy.Validation.CSharp
{
    public class MetadataCollection<TIdentifier> : IMetadataCollection<TIdentifier>
    {
        private readonly ConcurrentDictionary<TIdentifier, ConcurrentDictionary<string, object>> storage
            = new ConcurrentDictionary<TIdentifier, ConcurrentDictionary<string, object>>();

        public object this[TIdentifier identifier, string key]
        {
            get { return GetProperty(identifier, key); }
            set { SetProperty(identifier, key, value); }
        }

        private object GetProperty(TIdentifier identifier, string key)
        {
            if (storage.TryGetValue(identifier, out var properties) && properties.TryGetValue(key, out var value))
            {
                return value;
            }

            return null;
        }

        private void SetProperty(TIdentifier Identifier, string key, object value)
        {
            var properties = storage.GetOrAdd(Identifier, n => new ConcurrentDictionary<string, object>());
            properties.AddOrUpdate(key, value, (n, o) => value);
        }
    }
}