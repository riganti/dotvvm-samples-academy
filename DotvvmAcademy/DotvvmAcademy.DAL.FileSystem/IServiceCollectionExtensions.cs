using DotvvmAcademy.DAL.Base;
using DotvvmAcademy.DAL.Base.Entities;
using DotvvmAcademy.DAL.Base.Providers;
using DotvvmAcademy.DAL.FileSystem.Entities;
using DotvvmAcademy.DAL.FileSystem.Index;
using DotvvmAcademy.DAL.FileSystem.Index.Items;
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
            AddIndices(services);
            AddProviders(services);
        }

        private static void AddIndices(IServiceCollection services)
        {
            services.AddSingleton<IIndex<ILesson>, LessonIndex>();
            services.AddSingleton<IIndex<IStep>, StepIndex>();
            services.AddSingleton<IIndex<ISample>, SampleIndex>();
            services.AddSingleton<IIndex<ICSharpSourcePart>, SourcePartIndex>();
        }

        private static void AddProviders(IServiceCollection services)
        {
            services.AddSingleton<IEntityProvider<ILesson>, FileSystemEntityProvider<FileSystemLesson>>();
            services.AddSingleton<IEntityProvider<IStep>, FileSystemEntityProvider<FileSystemStep>>();
            services.AddSingleton<IEntityProvider<ISample>, FileSystemEntityProvider<FileSystemSample>>();
            services.AddSingleton<IEntityProvider<ICSharpSourcePart>, FileSystemEntityProvider<FileSystemCSharpSourcePart>>();
        }
    }
}