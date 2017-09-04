using Microsoft.Extensions.DependencyInjection;

namespace DotvvmAcademy.DAL.Base
{
    public static class IServiceCollectionExtensions
    {
        public static void AddDALBase(this IServiceCollection services)
        {
            services.AddSingleton<DotvvmAcademyContext>();
        }
    }
}