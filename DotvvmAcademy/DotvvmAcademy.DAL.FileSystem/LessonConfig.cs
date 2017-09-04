using Newtonsoft.Json;
using System.Collections.Generic;

namespace DotvvmAcademy.DAL.FileSystem
{
    public class LessonConfig
    {
        public string Annotation { get; set; }

        public string ImageUrl { get; set; }

        public bool IsReady { get; set; }

        public string Language { get; set; }

        public string Moniker { get; set; }

        [JsonIgnore]
        public string Path { get; set; }

        [JsonIgnore]
        public int FileIndex { get; set; }

        public string Name { get; set; }

        public string Project { get; set; }

        public List<string> Steps { get; set; }

        public string ValidatorProject { get; set; }
    }
}