using DotvvmAcademy.CourseFormat;
using DotvvmAcademy.Validation.CSharp;
using DotvvmAcademy.Validation.Dothtml;
using System.IO;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCourseFormat(this IServiceCollection services, string courseRoot)
        {
            var root = new DirectoryInfo(courseRoot);
            services.AddSingleton<CourseCache>();
            services.AddSingleton<CourseWorkspace>();
            services.AddSingleton<CodeTaskValidator>();
            services.AddSingleton<CSharpValidationService>();
            services.AddSingleton<DothtmlValidationService>();
            return services;
        }
    }
}