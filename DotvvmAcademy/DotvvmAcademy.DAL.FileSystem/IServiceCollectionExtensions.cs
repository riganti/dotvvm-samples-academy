using DotvvmAcademy.DAL.Base;
using DotvvmAcademy.DAL.Base.Entities;
using DotvvmAcademy.DAL.Base.Providers;
using DotvvmAcademy.DAL.FileSystem.Providers;
using DotvvmAcademy.DAL.FileSystem.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DotvvmAcademy.DAL.FileSystem
{
    public static class IServiceCollectionExtensions
    {
        public static void AddDALFileSystem(this IServiceCollection services)
        {
            services.AddDALBase();
            services.AddSingleton<ContentDirectoryEnvironment>();
            services.AddSingleton<IEntityProvider<ILesson>, FileSystemLessonProvider>();
            services.AddSingleton<IEntityProvider<IStep>, FileSystemStepProvider>();
            services.AddSingleton<IEntityProvider<ISample>, FileSystemSampleProvider>();
            services.AddSingleton<IEntityProvider<IProject>, FileSystemProjectProvider>();
            services.AddSingleton<IEntityProvider<IValidatorAssembly>, FileSystemValidatorAssemblyProvider>();
            services.AddSingleton<IEntityProvider<IValidator>, FileSystemValidatorProvider>();
            services.AddSingleton<IEntityProvider<ICSharpSourcePart>, FileSystemSourcePartProvider<ICSharpSourcePart>>();
            services.AddSingleton<IEntityProvider<IDothtmlSourcePart>, FileSystemSourcePartProvider<IDothtmlSourcePart>>();
            services.AddSingleton<IEntityProvider<IMvvmSourcePart>, FileSystemSourcePartProvider<IMvvmSourcePart>>();
        }
    }
}