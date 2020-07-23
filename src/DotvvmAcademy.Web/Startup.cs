using DotvvmAcademy.CourseFormat;
using DotvvmAcademy.Web.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.IO;

namespace DotvvmAcademy.Web
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app, CourseWorkspace workspace)
        {
            app.UseStaticFiles();
            app.UseDotVVM<DotvvmStartup>();

            workspace.LoadCourse("./Course")
                .GetAwaiter()
                .GetResult();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplicationInsightsTelemetry();
            services.AddDotVVM<DotvvmStartup>();
            services.AddSingleton<CourseWorkspace>();
            services.AddSingleton<ArchivePresenter>();
            services.AddSingleton<EmbeddedViewBuilder>();
            services.AddSingleton<EmbeddedViewCompiler>();
            services.AddSingleton<EmbeddedViewPresenter>();
            services.AddSingleton<EmbeddedViewTreeBuilder>();
            services.AddSingleton<EmbeddedViewTreeResolver>();
            services.AddSingleton<EmbeddedViewModelLoader>();
        }
    }
}
