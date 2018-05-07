using System.Collections.Generic;

namespace DotvvmAcademy.Validation
{
    public interface IMetadataCollection<TIdentifier> : IEnumerable<KeyValuePair<TIdentifier, IEnumerable<KeyValuePair<string, object>>>>
    {
        object this[TIdentifier identifier, string key] { get; set; }

        TProperty GetProperty<TProperty>(TIdentifier identifier, string key);

        TProperty GetRequiredProperty<TProperty>(TIdentifier identifier, string key);
    }
}