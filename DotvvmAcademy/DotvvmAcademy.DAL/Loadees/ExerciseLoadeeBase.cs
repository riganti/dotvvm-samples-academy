using System.IO;

namespace DotvvmAcademy.DAL.Loadees
{
    public abstract class ExerciseLoadeeBase
    {
        public string DisplayName { get; set; }

        public FileInfo Final { get; set; }

        public FileInfo Initial { get; set; }

        public string ValidatorId { get; set; }
    }
}