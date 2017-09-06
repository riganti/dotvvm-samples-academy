using DotvvmAcademy.CommonMark;
using DotvvmAcademy.CommonMark.ComponentParsers;
using DotvvmAcademy.DAL.Components;
using DotvvmAcademy.DAL.Loadees;
using DotvvmAcademy.DAL.Services;
using System.IO;
using System.Threading.Tasks;

namespace DotvvmAcademy.DAL.Loaders
{
    public class StepLoader : ILoader
    {
        private readonly ComponentizedConverter converter;
        private readonly ContentDirectoryEnvironment environment;

        public StepLoader(ContentDirectoryEnvironment environment, ComponentizedConverter converter)
        {
            this.environment = environment;
            this.converter = converter;
            converter.Use<XmlComponentParser<CSharpExerciseComponent>>();
            converter.Use<XmlComponentParser<DothtmlExerciseComponent>>();
            converter.Use<XmlComponentParser<MvvmExerciseComponent>>();
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
                step.Components = await converter.Convert(markdown);
                step.File = file;
            }
            return step;
        }
    }
}