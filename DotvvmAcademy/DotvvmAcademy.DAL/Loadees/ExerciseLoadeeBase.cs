using Newtonsoft.Json;
using System.IO;

namespace DotvvmAcademy.DAL.Loadees
{
    public abstract class ExerciseLoadeeBase
    {
        [JsonProperty("@Correct", Required = Required.Always)]
        public FileInfo Correct { get; set; }

        [JsonProperty("@Incorrect", Required = Required.Always)]
        public FileInfo Incorrect { get; set; }

        [JsonProperty(@"ValidatorId")]
        public string ValidatorId { get; set; }
    }
}