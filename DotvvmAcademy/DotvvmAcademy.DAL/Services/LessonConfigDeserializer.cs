using DotvvmAcademy.DAL.Loadees;
using Newtonsoft.Json;
using System.IO;
using System.Threading.Tasks;

namespace DotvvmAcademy.DAL.Services
{
    public class LessonConfigDeserializer
    {
        private readonly LessonConfigPathConverter pathConverter;

        public LessonConfigDeserializer(LessonConfigPathConverter pathConverter)
        {
            this.pathConverter = pathConverter;
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
                    pathConverter.Path = file.Directory.FullName;
                    var serializer = new JsonSerializer();
                    serializer.Converters.Add(pathConverter);
                    return serializer.Deserialize<LessonConfig>(jsonTextReader);
                }
            });
        }
    }
}