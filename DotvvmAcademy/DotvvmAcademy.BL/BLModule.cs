using Autofac;
using AutoMapper;
using DotvvmAcademy.BL.Facades;
using DotvvmAcademy.DAL;

namespace DotvvmAcademy.BL
{
    public class BLModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule<DalModule>();

            builder.RegisterAssemblyTypes(typeof(BLModule).Assembly)
                .Where(t => t.IsAssignableTo<IFacade>())
                .AsSelf();

            builder.RegisterAssemblyTypes(typeof(BLModule).Assembly)
                .Where(t => t.IsAssignableTo<Profile>())
                .As<Profile>()
                .SingleInstance();
        }
    }
}