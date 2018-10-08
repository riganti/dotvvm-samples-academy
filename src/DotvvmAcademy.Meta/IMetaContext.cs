using Microsoft.CodeAnalysis;
using System.Collections.Generic;
using System.Reflection;

namespace DotvvmAcademy.Meta
{
    public interface IMetaContext
    {
        IEnumerable<Assembly> Assemblies { get; set; }

        Compilation Compilation { get; set; }
    }
}