using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace DotvvmAcademy.Course.ProfileDetail
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseDotVVM<DotvvmStartup>(env.ContentRootPath);
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDotVVM();
        }
    }
}
