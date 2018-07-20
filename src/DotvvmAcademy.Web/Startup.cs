using DotvvmAcademy.CourseFormat;
using DotvvmAcademy.Validation;
using DotvvmAcademy.Validation.CSharp;
using DotvvmAcademy.Validation.CSharp.Unit;
using DotvvmAcademy.Validation.Dothtml;
using DotvvmAcademy.Validation.Dothtml.Unit;
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

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(env.WebRootPath)
            });

            var dotvvmConfiguration = app.UseDotVVM<DotvvmStartup>(env.ContentRootPath);
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDataProtection();
            services.AddAuthorization();
            services.AddWebEncoders();
            services.AddDotVVM();
            services.AddSingleton<MarkdownExtractor>();
            services.AddSingleton(new CourseWorkspace("../../sample/sample_course"));
            services.AddSingleton<CodeTaskValidator>();
            services.AddSingleton<IValidationService<CSharpUnit>, CSharpValidationService>();
            services.AddSingleton<IValidationService<DothtmlUnit>, DothtmlValidationService>();
        }
    }
}