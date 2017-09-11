using AutoMapper;
using DotvvmAcademy.DAL.Entities;
using DotvvmAcademy.DAL.Loadees;
using DotvvmAcademy.DAL.Loaders;
using DotvvmAcademy.DAL.Services;
using System.IO;
using System.Threading.Tasks;

namespace DotvvmAcademy.DAL.Providers
{
    public class StepProvider : IEntityProvider<Step>
    {
        private readonly StepLoader stepLoader;
        private readonly ContentDirectoryEnvironment environment;

        public StepProvider(StepLoader stepLoader, ContentDirectoryEnvironment environment)
        {
            this.stepLoader = stepLoader;
            this.environment = environment;
        }

        public async Task<Step> Get(string path)
        {
            var file = environment.GetAbsolute<FileInfo>(path);
            var source = await stepLoader.LoadStep(file);
            return Mapper.Map<StepLoadee, Step>(source);
        }
    }
}