using DotvvmAcademy.DAL.Loadees;
using System.IO;
using System.Threading.Tasks;

namespace DotvvmAcademy.DAL.Loaders
{
    public class SampleLoader : ILoader
    {
        public async Task<SampleSource> LoadSample(FileInfo file)
        {
            if (!file.Exists)
            {
                return null;
            }

            var sample = new SampleSource { File = file };
            using (var streamReader = file.OpenText())
            {
                sample.Source = await streamReader.ReadToEndAsync();
            }
            return sample;
        }
    }
}