using DotvvmAcademy.Meta.Syntax;
using System.Reflection;

namespace DotvvmAcademy.Meta
{
    public interface IMemberInfoNameBuilder
    {
        NameNode Build(MemberInfo memberInfo);
    }
}