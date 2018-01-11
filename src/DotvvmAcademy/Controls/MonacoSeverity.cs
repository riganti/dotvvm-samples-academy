using Newtonsoft.Json;

namespace DotvvmAcademy.Controls
{
    [JsonConverter(typeof(IntEnumConverter))]
    public enum MonacoSeverity
    {
        Ignore = 0,
        Info = 1,
        Warning = 2,
        Error = 3
    }
}