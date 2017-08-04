using DotvvmAcademy.BL.Facades;
using DotvvmAcademy.DAL.FileSystem;
using Microsoft.Extensions.DependencyInjection;

namespace DotvvmAcademy.BL
{
    public static class IServiceCollectionExtensions
    {
        public static void AddBL(this IServiceCollection services)
        {
            services.AddDALFileSystem();
            services.AddSingleton<LessonFacade>();
            services.AddSingleton<StepFacade>();
            services.AddSingleton<SampleFacade>();
        }
    }
}