using Newtonsoft.Json;
using System;

namespace DotvvmAcademy
{
    public class IntEnumConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType.IsEnum;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return Enum.ToObject(objectType, reader.ReadAsInt32());
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue((int)value);
        }
    }
}