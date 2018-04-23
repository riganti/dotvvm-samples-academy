using System;
using System.Collections.Generic;
using System.Text;

namespace DotvvmAcademy.Validation
{
    public interface IMetadataCollection<TIdentifier>
    {
        object this[TIdentifier identifier, string key] { get; set; }
    }
}
