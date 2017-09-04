using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace DotvvmAcademy.DAL.FileSystem.Services
{
    public class LessonConfigDeserializer
    {
        public Task<IEnumerable<LessonConfig>> Deserialize(StreamReader streamReader)
        {
            return Task.Run(() =>
            {
                using (streamReader)
                {
                    var jsonTextReader = new JsonTextReader(streamReader);
                    var serializer = new JsonSerializer();
                    return serializer.Deserialize<IEnumerable<LessonConfig>>(jsonTextReader);
                }
            });
        }
    }
}