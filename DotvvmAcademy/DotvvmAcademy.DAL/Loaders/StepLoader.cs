using DotvvmAcademy.CommonMark;
using DotvvmAcademy.DAL.Loadees;
using DotvvmAcademy.DAL.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DotvvmAcademy.DAL.Loaders
{
    public class StepLoader : ILoader
    {
        private readonly Func<SegmentizedConverterBuilder> builderFactory;
        private readonly ContentDirectoryEnvironment environment;
        private readonly Func<string, PathConverter> pathConverterFactory;
        private readonly Func<string, ExercisePlaceholderParser> parserFactory;

        public StepLoader(ContentDirectoryEnvironment environment, Func<SegmentizedConverterBuilder> builderFactory, Func<string, PathConverter> pathConverterFactory, Func<string, ExercisePlaceholderParser> parserFactory)
        {
            this.environment = environment;
            this.builderFactory = builderFactory;
            this.pathConverterFactory = pathConverterFactory;
            this.parserFactory = parserFactory;
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
                var converter = GetConverter(file);
                var markdown = await streamReader.ReadToEndAsync();
                step.Source = (await converter.Convert(markdown)).ToArray();
                step.File = file;
            }
            return step;
        }

        private SegmentizedConverter GetConverter(FileInfo file)
        {
            var builder = builderFactory();
            var pathConverter = pathConverterFactory(file.Directory.FullName);
            var parser = parserFactory(file.Directory.FullName);
            builder.UsePlaceholderParser(parser);
            return builder.Build();
        }
    }
}