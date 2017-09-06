using Newtonsoft.Json;
using System;
using System.IO;

namespace DotvvmAcademy.DAL.Services
{
    public class LessonConfigPathConverter : JsonConverter
    {
        public string Path { get; set; }

        public override bool CanConvert(Type objectType)
        {
            return typeof(FileSystemInfo).IsAssignableFrom(objectType);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var relativePath = (string)reader.Value;
            var path = System.IO.Path.Combine(Path, relativePath);
            return Activator.CreateInstance(objectType, path);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}