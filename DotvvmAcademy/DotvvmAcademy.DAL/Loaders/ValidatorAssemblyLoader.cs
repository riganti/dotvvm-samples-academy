using DotvvmAcademy.DAL.Loadees;
using DotvvmAcademy.DAL.Services;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace DotvvmAcademy.DAL.Loaders
{
    public class ValidatorAssemblyLoader : ILoader
    {
        private readonly ContentDirectoryEnvironment environment;

        public const string ValidatorAssemblyName = "DotvvmAcademy.Validators";

        public ValidatorAssemblyLoader(ContentDirectoryEnvironment environment)
        {
            this.environment = environment;
        }

        public Task<ValidatorAssemblyLoadee> LoadValidatorAssembly(DirectoryInfo directory = null)
        {
            //var dllPath = await Build(GetProjectFile(directory ?? environment.ValidatorsDirectory).FullName);
            var dllPath = Assembly.Load(new AssemblyName(ValidatorAssemblyName)).Location;
            var validatorAssembly = new ValidatorAssemblyLoadee
            {
                Dll = new FileInfo(dllPath),
            };
            return Task.FromResult(validatorAssembly);
        }

        //private async Task<string> Build(string projectPath)
        //{
        //    var configuration = environment.IsDevelopment ? "Debug" : "Release";
        //    var properties = new Dictionary<string, string>
        //    {
        //        {"Configuration", configuration }
        //    };
        //    var projectInstance = new ProjectInstance(projectPath, properties, null);
        //    var data = new BuildRequestData(projectInstance, new[] { "Restore", "Build" });
        //    var submission = BuildManager.DefaultBuildManager.PendBuildRequest(data);
        //    var tcs = new TaskCompletionSource<BuildResult>();
        //    submission.ExecuteAsync(s =>
        //    {
        //        tcs.SetResult(s.BuildResult);
        //    }, null);

        //    var result = await tcs.Task;
        //    throw new NotImplementedException();
        //}

        private FileInfo GetProjectFile(DirectoryInfo directory)
        {
            return directory.EnumerateFiles("*.csproj").Single();
        }
    }
}