using DotvvmAcademy.Meta;
using DotvvmAcademy.Meta.Syntax;
using Microsoft.CodeAnalysis;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMeta(this IServiceCollection c)
        {
            c.AddSingleton<IMetaContext, MetaContext>();
            c.AddSingleton<IMetaConverter<ISymbol, MemberInfo>, RoslynReflectionConverter>();
            c.AddSingleton<IMetaConverter<MemberInfo, ISymbol>, ReflectionRoslynConverter>();
            c.AddSingleton<IMetaConverter<ISymbol, NameNode>, RoslynNameConverter>();
            c.AddSingleton<IMetaConverter<NameNode, ISymbol>, NameRoslynConverter>();
            c.AddSingleton<IMetaConverter<MemberInfo, NameNode>, ReflectionNameConverter>();
            c.AddSingleton<IMetaConverter<NameNode, MemberInfo>, NameReflectionConverter>();
            c.AddSingleton<IPositionConverter, PositionConverter>();
            c.AddSingleton<ITypedConstantExtractor, TypedConstantExtractor>();
            c.AddSingleton<IAttributeExtractor, AttributeExtractor>();
            return c;
        }
    }
}