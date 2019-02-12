using DotvvmAcademy.CourseFormat;
using DotvvmAcademy.Validation;
using DotvvmAcademy.Validation.CSharp;
using DotvvmAcademy.Validation.CSharp.Unit;
using DotvvmAcademy.Validation.Dothtml;
using DotvvmAcademy.Validation.Dothtml.Unit;
using System.IO;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCourseFormat(this IServiceCollection services, string courseRoot)
        {
            var root = new DirectoryInfo(courseRoot);
            services.AddSingleton<ICourseEnvironment>(new FileSystemEnvironment(root));
            services.AddSingleton<ISourceProvider<Course>, CourseProvider>();
            services.AddSingleton<ISourceProvider<Lesson>, LessonProvider>();
            services.AddSingleton<ISourceProvider<LessonVariant>, LessonVariantProvider>();
            services.AddSingleton<ISourceProvider<Step>, StepProvider>();
            services.AddSingleton<ISourceProvider<CodeTask>, CodeTaskProvider>();
            services.AddSingleton<ISourceProvider<Archive>, ArchiveProvider>();
            services.AddSingleton<CourseCache>();
            services.AddSingleton<CourseWorkspace>();
            services.AddSingleton<CodeTaskValidator>();
            services.AddSingleton<IMarkdownRenderer, MarkdigRenderer>();
            services.AddSingleton<CSharpValidationService>();
            services.AddSingleton<DothtmlValidationService>();
            return services;
        }
    }
}