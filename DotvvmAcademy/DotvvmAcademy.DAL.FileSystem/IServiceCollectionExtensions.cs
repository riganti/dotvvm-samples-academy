using DotvvmAcademy.DAL.Base;
using DotvvmAcademy.DAL.Base.Entities;
using DotvvmAcademy.DAL.Base.Providers;
using DotvvmAcademy.DAL.FileSystem.Index;
using DotvvmAcademy.DAL.FileSystem.Index.Items;
using DotvvmAcademy.DAL.FileSystem.Loaders;
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
            AddLoaders(services);
        }

        private static void AddIndices(IServiceCollection services)
        {
            services.AddSingleton<IIndex<ILesson>, LessonIndex>();
            services.AddSingleton<IIndex<IStep>, StepIndex>();
            services.AddSingleton<IIndex<ISample>, SampleIndex>();
            services.AddSingleton<IIndex<ISourcePart>, SourcePartIndex>();
        }

        private static void AddLoaders(IServiceCollection services)
        {
            services.AddSingleton<ILoader<ILesson, LessonItem>, LessonLoader>();
            services.AddSingleton<ILoader<IStep, StepItem>, StepLoader>();
            services.AddSingleton<ILoader<ISample, SampleItem>, SampleLoader>();
            services.AddSingleton<ILoader<ISourcePart, SourcePartItem>, SourcePartLoader>();
        }

        private static void AddProviders(IServiceCollection services)
        {
            services.AddSingleton<IEntityProvider<ILesson>, FileSystemEntityProvider<ILesson>>();
            services.AddSingleton<IEntityProvider<IStep>, FileSystemEntityProvider<IStep>>();
            services.AddSingleton<IEntityProvider<ISample>, FileSystemEntityProvider<ISample>>();
            services.AddSingleton<IEntityProvider<ISourcePart>, FileSystemEntityProvider<ISourcePart>>();
        }
    }
}