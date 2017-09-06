using Autofac;
using AutoMapper;
using DotvvmAcademy.CommonMark;
using DotvvmAcademy.DAL.Loaders;
using DotvvmAcademy.DAL.Providers;
using DotvvmAcademy.DAL.Services;

namespace DotvvmAcademy.DAL
{
    public class DalModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(DalModule).Assembly)
                .Where(t => t.IsAssignableTo<Profile>())
                .As<Profile>()
                .SingleInstance();

            builder.RegisterAssemblyTypes(typeof(DalModule).Assembly)
                .Where(t => t.IsAssignableTo<ILoader>())
                .AsSelf()
                .SingleInstance();

            builder.RegisterAssemblyTypes(typeof(DalModule).Assembly)
                .AsClosedTypesOf(typeof(IEntityProvider<>))
                .AsSelf()
                .SingleInstance();

            builder.RegisterType<LessonConfigDeserializer>()
                .SingleInstance();

            builder.RegisterType<LessonConfigPathConverter>()
                .SingleInstance();

            builder.RegisterType<ContentDirectoryEnvironment>()
                .SingleInstance();

            builder.RegisterType<ComponentizedConverter>()
                .SingleInstance();
        }
    }
}