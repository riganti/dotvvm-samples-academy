using Newtonsoft.Json;
using System.IO;

namespace DotvvmAcademy.DAL.Loadees
{
    public class LessonConfig
    {
        public string Annotation { get; set; }

        [JsonIgnore]
        public FileInfo File { get; set; }

        public string ImageUrl { get; set; }

        public bool IsReady { get; set; }

        [JsonIgnore]
        public string Language { get; set; }

        [JsonIgnore]
        public string Moniker { get; set; }

        public string Name { get; set; }

        public FileInfo[] Steps { get; set; }
    }
}