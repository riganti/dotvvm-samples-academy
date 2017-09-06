using AutoMapper;
using DotvvmAcademy.DAL.Entities;
using DotvvmAcademy.DAL.Loadees;
using DotvvmAcademy.DAL.Loaders;
using DotvvmAcademy.DAL.Services;
using System.IO;
using System.Threading.Tasks;

namespace DotvvmAcademy.DAL.Providers
{
    public class ValidatorAssemblyProvider : IEntityProvider<ValidatorAssembly>
    {
        private readonly ContentDirectoryEnvironment environment;
        private readonly ValidatorAssemblyLoader loader;

        public ValidatorAssemblyProvider(ValidatorAssemblyLoader loader, ContentDirectoryEnvironment environment)
        {
            this.loader = loader;
            this.environment = environment;
        }

        public async Task<ValidatorAssembly> Get(string validatorProjectDirectoryPath = null)
        {
            DirectoryInfo directory = null;
            if (validatorProjectDirectoryPath != null)
            {
                directory = new DirectoryInfo(Path.Combine(environment.ContentDirectory.FullName, validatorProjectDirectoryPath));
            }
            var loadee = await loader.LoadValidatorAssembly(directory);
            return Mapper.Map<ValidatorAssemblyLoadee, ValidatorAssembly>(loadee);
        }
    }
}