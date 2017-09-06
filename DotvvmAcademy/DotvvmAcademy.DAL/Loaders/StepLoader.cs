using DotvvmAcademy.CommonMark;
using DotvvmAcademy.DAL.Loadees;
using DotvvmAcademy.DAL.Services;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DotvvmAcademy.DAL.Loaders
{
    public class StepLoader : ILoader
    {
        private readonly SegmentizedConverter converter;
        private readonly ContentDirectoryEnvironment environment;

        public StepLoader(ContentDirectoryEnvironment environment, SegmentizedConverterBuilder converterBuilder)
        {
            this.environment = environment;
            converter = converterBuilder.Build();
        }

        public async Task<StepSource> LoadStep(FileInfo file)
        {
            if (!file.Exists)
            {
                return null;
            }

            var step = new StepSource();
            using (var streamReader = file.OpenText())
            {
                var markdown = await streamReader.ReadToEndAsync();
                step.Source = (await converter.Convert(markdown)).ToArray();
                step.File = file;
            }
            return step;
        }
    }
}