using DotvvmAcademy.DAL.Base.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace DotvvmAcademy.DAL.Base.Services
{
    public class JsonLessonDeserializer : ILessonDeserializer
    {
        public IEnumerable<Lesson> Deserialize(string rawFile) 
            => JsonConvert.DeserializeObject<IEnumerable<Lesson>>(rawFile);
    }
}