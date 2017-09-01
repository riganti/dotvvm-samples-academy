using DotvvmAcademy.BL;
using DotvvmAcademy.BL.DTO.Components;
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
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IServiceProvider serviceProvider)
        {
            //CacheLessons(app);
            var applicationPhysicalPath = env.ContentRootPath;

            // use DotVVM
            var dotvvmConfiguration = app.UseDotVVM<DotvvmStartup>(applicationPhysicalPath);

            serviceProvider.GetService<ValidatorsBuilder>().BuildValidators().Wait();

            // use static files
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(applicationPhysicalPath, "wwwroot"))
            });
        }

        public void ConfigureComponentRenderers(IServiceCollection services)
        {
            services.AddTransient<SourceComponentRenderer<SampleComponent>, SampleComponentRenderer>();
            services.AddTransient<SourceComponentRenderer<HtmlLiteralComponent>, HtmlLiteralComponentRenderer>();
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

            services.AddSingleton<ValidatorsBuilder>();
            services.AddBL();
            ConfigureComponentRenderers(services);
        }
    }
}