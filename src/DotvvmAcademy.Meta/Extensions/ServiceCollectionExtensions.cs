using DotvvmAcademy.Meta;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMetaScopeFriendly(this IServiceCollection container)
        {
            container.AddSingleton<ISymbolNameBuilder, SymbolNameBuilder>();
            container.AddSingleton<IMemberInfoNameBuilder, MemberInfoNameBuilder>();
            container.AddSingleton<PropertyEqualityComparer>();

            container.AddScoped<ISymbolLocator, SymbolLocator>();
            container.AddScoped<ISymbolConverter, SymbolConverter>();
            container.AddScoped<IMemberInfoLocator, MemberInfoLocator>();
            container.AddScoped<IMemberInfoConverter, MemberInfoConverter>();
            container.AddScoped<ITypedConstantExtractor, TypedConstantExtractor>();
            container.AddScoped<ITypedAttributeExtractor, TypedAttributeExtractor>();
            container.AddScoped<IAttributeExtractor>(p => p.GetRequiredService<ITypedAttributeExtractor>());
            container.AddScoped<ICSharpCompilationAccessor, CSharpCompilationAccessor>();
            container.AddScoped<IAssemblyAccessor, AssemblyAccessor>();

            return container;
        }

        public static IServiceCollection AddMetaSingletonFriendly(this IServiceCollection container)
        {
            container.AddSingleton<ISymbolLocator, SymbolLocator>();
            container.AddSingleton<ISymbolNameBuilder, SymbolNameBuilder>();
            container.AddSingleton<ISymbolConverter, SymbolConverter>();
            container.AddSingleton<IMemberInfoLocator, MemberInfoLocator>();
            container.AddSingleton<IMemberInfoNameBuilder, MemberInfoNameBuilder>();
            container.AddSingleton<IMemberInfoConverter, MemberInfoConverter>();
            container.AddSingleton<ITypedConstantExtractor, TypedConstantExtractor>();
            container.AddScoped<ITypedAttributeExtractor, TypedAttributeExtractor>();
            container.AddScoped<IAttributeExtractor>(p => p.GetRequiredService<ITypedAttributeExtractor>());
            container.AddSingleton<ICSharpCompilationAccessor, CSharpCompilationAccessor>();
            container.AddSingleton<IAssemblyAccessor, AssemblyAccessor>();
            container.AddSingleton<PropertyEqualityComparer>();

            return container;
        }
    }
}