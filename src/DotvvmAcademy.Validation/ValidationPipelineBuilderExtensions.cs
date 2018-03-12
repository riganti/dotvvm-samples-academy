using Microsoft.Extensions.DependencyInjection;
using System;

namespace DotvvmAcademy.Validation
{
    public static class ValidationPipelineBuilderExtensions
    {
        public static IValidationPipelineBuilder UseMiddleware(
            this IValidationPipelineBuilder builder,
            Type middlewareType)
        {
            builder.Use(next =>
            {
                return context =>
                {
                    var middleware = (IValidationMiddleware)context
                        .GetRequiredItem<IServiceProvider>(MiddlewareValidationService.ServicesKey)
                        .GetRequiredService(middlewareType);
                    return middleware.InvokeAsync(context, next);
                };
            });
            return builder;
        }

        public static IValidationPipelineBuilder UseMiddleware<TMiddleware>(this IValidationPipelineBuilder builder)
            where TMiddleware : IValidationMiddleware
        {
            return UseMiddleware(builder, typeof(TMiddleware));
        }
    }
}