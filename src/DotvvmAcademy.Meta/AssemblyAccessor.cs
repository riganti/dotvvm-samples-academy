using System.Collections.Immutable;
using System.Reflection;

namespace DotvvmAcademy.Meta
{
    public class AssemblyAccessor : IAssemblyAccessor
    {
        public ImmutableArray<Assembly> Assemblies { get; set; }
    }
}