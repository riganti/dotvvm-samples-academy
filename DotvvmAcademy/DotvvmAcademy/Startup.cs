using Autofac;
using AutoMapper;
using DotVVM.Framework.ViewModel;
using DotvvmAcademy.BL;
using DotvvmAcademy.Controls;
using DotvvmAcademy.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using System;
using System.IO;

namespace DotvvmAcademy
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, AutoMapperInitializer autoMapperInitializer)
        {
            env.EnvironmentName = EnvironmentName.Development;
            autoMapperInitializer.Initialize();
            var applicationPhysicalPath = env.ContentRootPath;
            var dotvvmConfiguration = app.UseDotVVM<DotvvmStartup>(applicationPhysicalPath);
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(applicationPhysicalPath, "wwwroot"))
            });
            app.UseStatusCodePagesWithRedirects("/error/{0}");
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDataProtection();
            services.AddAuthorization();
            services.AddWebEncoders();
            services.AddDotVVM(options =>
            {
                options.AddDefaultTempStorages("temp");
            });
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule<BLModule>();
            builder.RegisterType<AutoMapperInitializer>();

            builder.RegisterAssemblyTypes(typeof(Startup).Assembly)
                .AsClosedTypesOf(typeof(StepPartRenderer<>));

            builder.RegisterAssemblyTypes(typeof(Startup).Assembly)
                .AssignableTo<IDotvvmViewModel>()
                .AsSelf();
        }
    }
}