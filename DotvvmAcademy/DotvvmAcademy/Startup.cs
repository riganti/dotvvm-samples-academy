using DotvvmAcademy.BL;
using DotvvmAcademy.BL.DTO.Components;
using DotvvmAcademy.Controls;
using DotvvmAcademy.ViewModels;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using System.IO;

namespace DotvvmAcademy
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            //CacheLessons(app);
            var applicationPhysicalPath = env.ContentRootPath;

            // use DotVVM
            var dotvvmConfiguration = app.UseDotVVM<DotvvmStartup>(applicationPhysicalPath);

            // use static files
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(applicationPhysicalPath, "wwwroot"))
            });
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

            services.AddBL();
            ConfigureComponentRenderers(services);
        }

        public void ConfigureComponentRenderers(IServiceCollection services)
        {
            services.AddTransient<SourceComponentRenderer<SampleComponent>, SampleComponentRenderer>();
            services.AddTransient<SourceComponentRenderer<HtmlLiteralComponent>, HtmlLiteralComponentRenderer>();
        }

        //private void CacheLessons(IApplicationBuilder app)
        //{
        //    var lessonCache = app.ApplicationServices.GetService<ILessonCache>();
        //    var stepCache = app.ApplicationServices.GetService<IStepCache>();
        //    var lessonProvider = app.ApplicationServices.GetService<ILessonProvider>();
        //    var stepProvider = app.ApplicationServices.GetService<IStepProvider>();

        //    var lessons = lessonProvider.GetLessons();
        //    foreach (var lesson in lessons)
        //    {
        //        lessonCache.Add(lesson);
        //        var steps = stepProvider.GetSteps(lesson).ToList();
        //        for (int i = 0; i < steps.Count; i++)
        //        {
        //            stepCache.Add(steps[i], lesson, i);
        //        }
        //    }
        //}
    }
}