using System.Collections.Immutable;
using System.Reflection;

namespace DotvvmAcademy.Meta
{
    public interface IAssemblyAccessor
    {
        ImmutableArray<Assembly> Assemblies { get; set; }
    }
}