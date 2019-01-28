using DotvvmAcademy.Web.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace DotvvmAcademy.Web
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app)
        {
            app.UseStaticFiles();
            app.UseDotVVM<DotvvmStartup>();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDotVVM<DotvvmStartup>();
            services.AddCourseFormat("./Course");
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