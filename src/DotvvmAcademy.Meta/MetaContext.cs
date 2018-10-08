using Microsoft.CodeAnalysis;
using System.Collections.Generic;
using System.Reflection;

namespace DotvvmAcademy.Meta
{
    public class MetaContext : IMetaContext
    {
        public IEnumerable<Assembly> Assemblies { get; set; }

        public Compilation Compilation { get; set; }
    }
}