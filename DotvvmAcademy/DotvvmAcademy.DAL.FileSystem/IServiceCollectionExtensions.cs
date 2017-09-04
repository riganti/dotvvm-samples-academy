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
            services.AddSingleton<IEntityProvider<Lesson>, FileSystemLessonProvider>();
            services.AddSingleton<IEntityProvider<Step>, FileSystemStepProvider>();
            services.AddSingleton<IEntityProvider<Sample>, FileSystemSampleProvider>();
            services.AddSingleton<IEntityProvider<Project>, FileSystemProjectProvider>();
            services.AddSingleton<IEntityProvider<ValidatorAssembly>, FileSystemValidatorAssemblyProvider>();
            services.AddSingleton<IEntityProvider<Validator>, FileSystemValidatorProvider>();
            services.AddSingleton<IEntityProvider<CSharpSampleSourcePart>, FileSystemSourcePartProvider<CSharpSampleSourcePart>>();
            services.AddSingleton<IEntityProvider<DothtmlSampleSourcePart>, FileSystemSourcePartProvider<DothtmlSampleSourcePart>>();
            services.AddSingleton<IEntityProvider<MvvmSampleSourcePart>, FileSystemSourcePartProvider<MvvmSampleSourcePart>>();
        }
    }
}