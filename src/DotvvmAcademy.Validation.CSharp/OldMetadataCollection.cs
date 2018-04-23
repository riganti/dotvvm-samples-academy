using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace DotvvmAcademy.Validation.CSharp
{
    public sealed class OldMetadataCollection
    {
        private ConcurrentDictionary<MetadataName, ConcurrentDictionary<string, object>> primaryStorage
            = new ConcurrentDictionary<MetadataName, ConcurrentDictionary<string, object>>();

        private ConcurrentDictionary<string, HashSet<MetadataName>> secondaryStorage
            = new ConcurrentDictionary<string, HashSet<MetadataName>>();

        public IEnumerable<string> this[MetadataName name]
        {
            get { return GetPropertiesOfName(name); }
        }

        public IEnumerable<MetadataName> this[string key]
        {
            get { return GetNamesWithProperty(key); }
        }

        public object this[MetadataName name, string key]
        {
            get { return GetProperty(name, key); }
            set { SetProperty(name, key, value); }
        }

        public IEnumerable<MetadataName> GetNames()
        {
            return primaryStorage.Keys;
        }

        public IEnumerable<MetadataName> GetNamesWithProperty(string key)
        {
            if (secondaryStorage.TryGetValue(key, out var names))
            {
                return names;
            }

            return Enumerable.Empty<MetadataName>();
        }

        public IEnumerable<string> GetPropertiesOfName(MetadataName name)
        {
            if (primaryStorage.TryGetValue(name, out var properties))
            {
                return properties.Keys;
            }

            return Enumerable.Empty<string>();
        }

        public object GetProperty(MetadataName name, string key)
        {
            if (primaryStorage.TryGetValue(name, out var properties) && properties.TryGetValue(key, out var value))
            {
                return value;
            }

            return null;
        }

        public TProperty RequireProperty<TProperty>(MetadataName name, string key)
        {
            var value = GetProperty(name, key);
            if (value == null)
            {
                throw new InvalidOperationException($"The value for the {key} property of {name} is not present.");
            }
            if (value is TProperty cast)
            {
                return cast;
            }

            throw new InvalidOperationException($"The value for the {key} property of {name} is not of type {typeof(TProperty)}.");
        }

        public void SetProperty(MetadataName name, string key, object value)
        {
            var properties = primaryStorage.GetOrAdd(name, n => new ConcurrentDictionary<string, object>());
            properties.AddOrUpdate(key, value, (n, o) => value);

            var names = secondaryStorage.GetOrAdd(key, (k) => new HashSet<MetadataName>());
            names.Add(name);
        }
    }
}