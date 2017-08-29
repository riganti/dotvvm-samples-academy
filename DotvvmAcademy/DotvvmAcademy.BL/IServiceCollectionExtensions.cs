using DotvvmAcademy.BL.CommonMark;
using DotvvmAcademy.BL.DTO.Components;
using DotvvmAcademy.BL.Facades;
using DotvvmAcademy.DAL.FileSystem;
using DotvvmAcademy.Validation;
using Microsoft.Extensions.DependencyInjection;

namespace DotvvmAcademy.BL
{
    public static class IServiceCollectionExtensions
    {
        public static void AddBL(this IServiceCollection services)
        {
            services.AddDALFileSystem();
            services.AddSingleton<LessonFacade>();
            services.AddSingleton<StepFacade>();
            services.AddSingleton<SampleFacade>();
            services.AddSingleton<ValidatorFacade>();
            services.AddSingleton<IComponentParser<SampleComponent>, SampleComponentParser>();
            services.AddSingleton(p =>
            {
                var parser = new StepParser();
                parser.RegisterComponentParser(p.GetService<IComponentParser<SampleComponent>>());
                return parser;
            });
            services.AddSingleton<ValidatorDelegateFactory>();
        }
    }
}