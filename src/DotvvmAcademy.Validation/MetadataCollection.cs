using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace DotvvmAcademy.Validation
{
    public class MetadataCollection<TIdentifier> : IMetadataCollection<TIdentifier>
    {
        private readonly ConcurrentDictionary<TIdentifier, IEnumerable<KeyValuePair<string, object>>> storage
            = new ConcurrentDictionary<TIdentifier, IEnumerable<KeyValuePair<string, object>>>();

        public object this[TIdentifier identifier, string key]
        {
            get { return GetProperty(identifier, key); }
            set { SetProperty(identifier, key, value); }
        }

        public IEnumerator<KeyValuePair<TIdentifier, IEnumerable<KeyValuePair<string, object>>>> GetEnumerator()
            => storage.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
            => storage.GetEnumerator();

        public TProperty GetProperty<TProperty>(TIdentifier identifier, string key)
        {
            var value = this[identifier, key];
            if (value is TProperty cast)
            {
                return cast;
            }
            throw new InvalidOperationException($"The '{key}' property for identifier '{identifier}' "
                + $"is of an unexpected type. Expected: '{typeof(TProperty)}'. Actual: '{value.GetType()}'");
        }

        public TProperty GetRequiredProperty<TProperty>(TIdentifier identifier, string key)
        {
            var value = GetProperty<TProperty>(identifier, key);
            if (value != null)
            {
                return value;
            }
            throw new InvalidOperationException($"The value of the '{key}' property for '{identifier}' is null.");
        }

        private object GetProperty(TIdentifier identifier, string key)
        {
            if (!storage.TryGetValue(identifier, out var pairs))
            {
                return null;
            }

            var properties = (ConcurrentDictionary<string, object>)pairs;
            if (!properties.TryGetValue(key, out var value))
            {
                return null;
            }

            return value;
        }

        private void SetProperty(TIdentifier Identifier, string key, object value)
        {
            var properties = (ConcurrentDictionary<string, object>)storage
                .GetOrAdd(Identifier, n => new ConcurrentDictionary<string, object>());
            properties.AddOrUpdate(key, value, (n, o) => value);
        }
    }
}