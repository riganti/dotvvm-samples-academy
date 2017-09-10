using Newtonsoft.Json;
using System.IO;

namespace DotvvmAcademy.DAL.Loadees
{
    public abstract class ExerciseLoadeeBase
    {
        public FileInfo Correct { get; set; }

        public FileInfo Incorrect { get; set; }

        public string ValidatorId { get; set; }
    }
}