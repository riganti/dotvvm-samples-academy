using AutoMapper;
using DotvvmAcademy.DAL.Entities;
using DotvvmAcademy.DAL.Loadees;
using DotvvmAcademy.DAL.Loaders;
using DotvvmAcademy.DAL.Services;
using System.IO;
using System.Threading.Tasks;

namespace DotvvmAcademy.DAL.Providers
{
    public class SampleProvider : IEntityProvider<Sample>
    {
        private readonly ContentDirectoryEnvironment environment;
        private readonly SampleLoader sampleLoader;

        public SampleProvider(SampleLoader sampleLoader, ContentDirectoryEnvironment environment)
        {
            this.sampleLoader = sampleLoader;
            this.environment = environment;
        }

        public async Task<Sample> Get(string path)
        {
            var file = new FileInfo(Path.Combine(environment.ContentDirectory.FullName, path));
            var source = await sampleLoader.LoadSample(file);
            return Mapper.Map<SampleSource, Sample>(source);
        }
    }
}