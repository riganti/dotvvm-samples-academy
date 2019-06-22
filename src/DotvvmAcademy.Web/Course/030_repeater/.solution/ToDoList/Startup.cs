using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace DotvvmAcademy.Course.ProfileDetail
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseDotVVM<DotvvmStartup>(env.ContentRootPath);
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDotVVM();
        }
    }
}
