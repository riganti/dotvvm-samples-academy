using Newtonsoft.Json;
using System;
using System.IO;

namespace DotvvmAcademy.DAL.Services
{
    public class PathConverter : JsonConverter
    {
        private readonly string basePath;

        public PathConverter(string basePath)
        {
            this.basePath = basePath;
        }

        public override bool CanConvert(Type objectType)
        {
            return typeof(FileSystemInfo).IsAssignableFrom(objectType);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var relativePath = (string)reader.Value;
            var absolutePath = Path.Combine(basePath, relativePath);
            return Activator.CreateInstance(objectType, absolutePath);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}