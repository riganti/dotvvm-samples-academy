using Microsoft.Extensions.DependencyInjection;

namespace DotvvmAcademy.Validation.CSharp.Tests
{
    public class TestValidationService : MiddlewareValidationService
    {
        public override void Configure(IValidationPipelineBuilder pipeline)
        {
            pipeline.UseMiddleware<CSharpCompilationMiddleware>();
            pipeline.UseMiddleware<StaticAnalysisMiddleware>();
            pipeline.UseMiddleware<AssemblyRewritingMiddleware>();
            pipeline.UseMiddleware<DynamicAnalysisMiddleware>();
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddStaticAnalysis();
            services.AddAssemblyRewriting();
            services.AddDynamicAnalysis();
        }
    }
}