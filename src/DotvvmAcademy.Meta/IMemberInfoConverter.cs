using Microsoft.CodeAnalysis;
using System.Reflection;

namespace DotvvmAcademy.Meta
{
    public interface IMemberInfoConverter
    {
        ISymbol Convert(MemberInfo memberInfo);
    }
}