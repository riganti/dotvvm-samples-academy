using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DotvvmAcademy.Services
{
    public class ValidatorsBuilder
    {
        private readonly string absoluteProjectPath;

        public const string RelativeProjectPath = "./Content/Validators/DotvvmAcademy.Lessons.Validators.csproj";
        private readonly IHostingEnvironment environment;

        public ValidatorsBuilder(IHostingEnvironment environment)
        {
            absoluteProjectPath = Path.Combine(environment.ContentRootPath, RelativeProjectPath);
            this.environment = environment;
        }

        public string ValidatorAssemblyPath { get; private set; }

        public Task BuildValidators()
        {
            var expected = GetExpectedValidatorAssemblyPath();
            if(File.Exists(expected))
            {
                ValidatorAssemblyPath = expected;
                return Task.CompletedTask;
            }

            var tcs = new TaskCompletionSource<object>();
            var process = new Process();
            process.StartInfo = new ProcessStartInfo
            {
                FileName = "dotnet",
                Arguments = $"build {absoluteProjectPath}",
                RedirectStandardOutput = true,
                RedirectStandardError = true
            };
            process.EnableRaisingEvents = true;
            process.Exited += (sender, args) =>
            {
                throw new NotImplementedException();
                tcs.TrySetResult(null);
            };
            process.Start();
            return tcs.Task;
        }

        private string GetExpectedValidatorAssemblyPath()
        {
            var relativeDllPath = $"./Content/Validators/bin/{GetConfiguration()}/netstandard2.0/DotvvmAcademy.Lessons.Validators.dll";
            return Path.Combine(environment.ContentRootPath, relativeDllPath);
        }

        private string GetConfiguration()
        {
            return environment.IsDevelopment() ? "Debug" : "Release";
        }
    }
}
