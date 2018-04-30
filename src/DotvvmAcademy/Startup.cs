using Autofac;
using DotVVM.Framework.Hosting;
using DotVVM.Framework.ViewModel;
using DotvvmAcademy.BL;
using DotvvmAcademy.Controls;
using DotvvmAcademy.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace DotvvmAcademy
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, AutoMapperInitializer autoMapperInitializer)
        {
            autoMapperInitializer.Initialize();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/error/500");
            }
            app.UseStaticFiles();
            app.UseDotVVM<DotvvmStartup>(env.ContentRootPath);
            app.UseStatusCodePagesWithRedirects("/error/{0}");
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule<BLModule>();
            builder.RegisterType<AutoMapperInitializer>();
            builder.RegisterAssemblyTypes(typeof(Startup).Assembly)
                .AssignableTo<IDotvvmViewModel>()
                .AsSelf();
            builder.RegisterType<DotvvmPresenter>()
                .SingleInstance();
            builder.RegisterInstance(LocalizedPresenter.BasedOnParameter("Language")())
                .As<IDotvvmPresenter>()
                .SingleInstance();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLocalization(options => options.ResourcesPath = "Resources");
            services.AddDataProtection();
            services.AddAuthorization();
            services.AddWebEncoders();
            services.AddDotVVM();
        }
    }
}