using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace DotvvmAcademy.Validation
{
    public abstract class MiddlewareValidationService : IValidationService
    {
        public const string ServicesKey = "Services";

        private readonly IServiceCollection serviceCollection;
        private readonly ValidationDelegate validate;

        public MiddlewareValidationService()
        {
            serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            AddCommonServices();
            Services = serviceCollection.BuildServiceProvider();
            var pipelineBuilder = Services.GetRequiredService<IValidationPipelineBuilder>();
            Configure(pipelineBuilder);
            validate = pipelineBuilder.Build();
        }

        public IServiceProvider Services { get; }

        public abstract void Configure(IValidationPipelineBuilder pipeline);

        public abstract void ConfigureServices(IServiceCollection services);

        public Task Validate(ValidationContext context)
        {
            using(var scope = Services.CreateScope())
            {
                context.SetItem(ServicesKey, scope.ServiceProvider);
                return validate(context);
            }
        }

        private void AddCommonServices()
        {
            serviceCollection.AddSingleton<IValidationPipelineBuilder, ValidationPipelineBuilder>();
        }
    }
}