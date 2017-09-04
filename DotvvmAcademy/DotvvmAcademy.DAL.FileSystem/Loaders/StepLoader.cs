using DotvvmAcademy.CommonMark;
using DotvvmAcademy.DAL.Base.Entities;
using DotvvmAcademy.DAL.FileSystem.Index.Items;
using DotvvmAcademy.DAL.FileSystem.Loaders;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace DotvvmAcademy.DAL.FileSystem.Services
{
    public class StepLoader : ILoader<IStep, StepItem>
    {
        private readonly ComponentizedConverter converter;

        public StepLoader()
        {
            converter = new ComponentizedConverter();
            converter.UseDefaultXmlParsers();
        }

        public Task<List<StepSource>> ConvertSteps(IEnumerable<string> paths)
        {
            var steps = new List<StepSource>();
            foreach (var step in steps)
            {

            }
        }

        public async Task<StepSource> ConvertStep(string path)
        {
            using (var streamReader = File.OpenText(path))
            {
                var markdown = await streamReader.ReadToEndAsync();
                var components = await converter.Convert(markdown);
                var step = new StepSource
                {
                    Path = path,
                    Components = components
                };
                return step;
            }
        }
    }
}
