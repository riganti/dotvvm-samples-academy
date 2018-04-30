using System;
using System.Collections.Generic;
using System.Text;

namespace DotvvmAcademy.Validation
{
    public static class MetadataCollectionExtensions
    {
        public static TProperty GetProperty<TIdentifier, TProperty>(this IMetadataCollection<TIdentifier> collection, TIdentifier identifier, string key)
        {
            var value = collection[identifier, key];
            if (value is TProperty cast)
            {
                return cast;
            }
            throw new InvalidOperationException($"The value of the '{key}' property for '{identifier}' is an instance of type {typeof(TProperty)}.");
        }

        public static TProperty GetRequiredProperty<TIdentifier, TProperty>(this IMetadataCollection<TIdentifier> collection, TIdentifier identifier, string key)
        {
            var value = collection.GetProperty<TIdentifier, TProperty>(identifier, key);
            if (value != null)
            {
                return value;
            }
            throw new InvalidOperationException($"The value of the '{key}' property for '{identifier}' is null.");
        }
    }
}
