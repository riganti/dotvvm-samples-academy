using DotvvmAcademy.DAL.Base;
using DotvvmAcademy.DAL.Base.Providers;
using DotvvmAcademy.DAL.Base.Services;
using DotvvmAcademy.DAL.FileSystem.Providers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace DotvvmAcademy.DAL.FileSystem
{
    public static class IServiceCollectionExtensions
    {
        public static void AddDALFileSystem(this IServiceCollection services)
        {
            services.AddDALBase();
            services.AddSingleton<ILessonProvider, FileSystemLessonProvider>(p =>
            {
                return new FileSystemLessonProvider(
                    p.GetService<IHostingEnvironment>().ContentRootPath,
                    p.GetService<ILessonDeserializer>());
            });
            services.AddSingleton<IStepProvider, FileSystemStepProvider>(p => 
            {
                return new FileSystemStepProvider(p.GetService<IHostingEnvironment>().ContentRootPath);
            });
            services.AddSingleton<ISampleProvider, FileSystemSampleProvider>(p =>
            {
                return new FileSystemSampleProvider(p.GetService<IHostingEnvironment>().ContentRootPath);
            });
        }
    }
}