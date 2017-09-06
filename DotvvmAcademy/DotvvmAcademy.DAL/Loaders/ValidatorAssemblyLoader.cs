using DotvvmAcademy.DAL.Loadees;
using DotvvmAcademy.DAL.Services;
using Microsoft.Build.Execution;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace DotvvmAcademy.DAL.Loaders
{
    public class ValidatorAssemblyLoader : ILoader
    {
        private readonly ContentDirectoryEnvironment environment;

        public ValidatorAssemblyLoader(ContentDirectoryEnvironment environment)
        {
            this.environment = environment;
        }

        public async Task<ValidatorAssemblyLoadee> LoadValidatorAssembly(DirectoryInfo directory = null)
        {
            var dllPath = await Build(directory?.FullName ?? environment.ValidatorsDirectory.FullName);
            var validatorAssembly = new ValidatorAssemblyLoadee
            {
                Dll = new FileInfo(dllPath),
            };
            return validatorAssembly;
        }

        private async Task<string> Build(string projectPath)
        {
            var configuration = environment.IsDevelopment ? "Debug" : "Release";
            var properties = new Dictionary<string, string>
            {
                {"Configuration", configuration }
            };
            var projectInstance = new ProjectInstance(projectPath, properties, null);
            var data = new BuildRequestData(projectInstance, new[] { "Restore", "Build" });
            var submission = BuildManager.DefaultBuildManager.PendBuildRequest(data);
            var tcs = new TaskCompletionSource<BuildResult>();
            submission.ExecuteAsync(s =>
            {
                tcs.SetResult(s.BuildResult);
            }, null);

            var result = await tcs.Task;
            throw new NotImplementedException();
        }
    }
}