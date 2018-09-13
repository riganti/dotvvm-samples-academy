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
            services.AddSingleton<EVPresenter>();
            services.AddScoped<EVHackService>();
            services.AddScoped<EVControlTreeResolver>();
            services.AddScoped<EVResolvedTreeBuilder>();
            services.AddScoped<EVViewCompiler>();
        }
    }
}