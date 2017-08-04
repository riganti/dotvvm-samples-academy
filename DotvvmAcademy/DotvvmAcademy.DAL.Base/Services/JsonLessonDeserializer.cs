using DotvvmAcademy.DAL.Base.Entities;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace DotvvmAcademy.DAL.Base.Services
{
    public class JsonLessonDeserializer : ILessonDeserializer
    {
        public IEnumerable<Lesson> Deserialize(string rawFile) => JsonConvert.DeserializeObject<IEnumerable<Lesson>>(rawFile);
    }
}