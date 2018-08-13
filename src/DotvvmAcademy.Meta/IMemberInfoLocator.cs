using DotvvmAcademy.Meta.Syntax;
using System.Collections.Immutable;
using System.Reflection;

namespace DotvvmAcademy.Meta
{
    public interface IMemberInfoLocator
    {
        ImmutableArray<MemberInfo> Locate(NameNode name);
    }
}