using DotvvmAcademy.Meta.Syntax;
using System.Collections.Immutable;

namespace DotvvmAcademy.Meta
{
    public interface ILocator<TMeta>
    {
        ImmutableArray<TMeta> Locate(NameNode node);
    }
}