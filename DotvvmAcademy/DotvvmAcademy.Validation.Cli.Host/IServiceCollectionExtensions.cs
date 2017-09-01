using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System.IO;

namespace DotvvmAcademy.Validation.Cli.Host
{
    public static class IServiceCollectionExtensions
    {
        public static void AddValidationCliHost(this IServiceCollection services)
        {
            services.AddTransient<ValidatorCli>();
        }
    }
}