using DotvvmAcademy.DAL.Loadees;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading.Tasks;

namespace DotvvmAcademy.DAL.Services
{
    public class LessonConfigDeserializer
    {
        private readonly Func<string, PathConverter> converterFactory;

        public LessonConfigDeserializer(Func<string, PathConverter> converterFactory)
        {
            this.converterFactory = converterFactory;
        }

        public Task<LessonConfig> Deserialize(FileInfo file)
        {
            if(!file.Exists)
            {
                return Task.FromResult<LessonConfig>(null);
            }

            return Task.Run(() =>
            {
                using (var streamReader = file.OpenText())
                {
                    var jsonTextReader = new JsonTextReader(streamReader);
                    var settings = new JsonSerializerSettings();
                    var converter = converterFactory(file.Directory.FullName);
                    var serializer = new JsonSerializer();
                    serializer.Converters.Add(converter);
                    return serializer.Deserialize<LessonConfig>(jsonTextReader);
                }
            });
        }
    }
}