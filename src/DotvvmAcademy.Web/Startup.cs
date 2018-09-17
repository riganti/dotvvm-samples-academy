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
            services.AddSingleton<EVViewBuilder>();
            services.AddSingleton<EVViewCompiler>();
            services.AddSingleton<EVControlTreeResolver>();
            services.AddSingleton<EVResolvedTreeBuilder>();
            services.AddScoped<RegularLifecycleStrategy>();
            services.AddScoped<PostbackLifecycleStrategy>();
            services.AddScoped<EVRegularLifecycleStrategy>();
            services.AddScoped<EVPostbackLifecycleStrategy>();
        }
    }
}