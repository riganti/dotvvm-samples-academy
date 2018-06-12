using DotvvmAcademy.BL;
using DotvvmAcademy.CourseFormat;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;

namespace DotvvmAcademy.Web
{
    public class Startup
    {
        public const string AuthenticationScheme = "Cookie";

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            var dotvvmConfiguration = app.UseDotVVM<DotvvmStartup>(env.ContentRootPath);

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(env.WebRootPath)
            });
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDataProtection();
            services.AddAuthorization();
            services.AddWebEncoders();
            services.AddDotVVM();
            services.AddSingleton<LessonFacade>();
            services.AddSingleton(new CourseWorkspace("../../sample/sample_course"));
        }
    }
}