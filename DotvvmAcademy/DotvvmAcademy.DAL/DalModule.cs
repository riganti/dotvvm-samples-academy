using Autofac;
using AutoMapper;
using DotvvmAcademy.CommonMark;
using DotvvmAcademy.DAL.Loaders;
using DotvvmAcademy.DAL.Providers;
using DotvvmAcademy.DAL.Services;
using System.Linq;

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

            builder.RegisterType<PathConverter>();

            builder.RegisterType<ContentDirectoryEnvironment>()
                .SingleInstance();

            builder.RegisterType<SegmentizedConverterBuilder>();

            builder.RegisterType<XmlNamingStrategy>();

            builder.RegisterType<ExercisePlaceholderParser>();
        }
    }
}