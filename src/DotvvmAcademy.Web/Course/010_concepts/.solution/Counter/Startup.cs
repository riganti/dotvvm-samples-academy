using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace DotvvmAcademy.Course.Counter
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDotVVM();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseDotVVM<DotvvmStartup>(env.ContentRootPath);
        }
    }
}
