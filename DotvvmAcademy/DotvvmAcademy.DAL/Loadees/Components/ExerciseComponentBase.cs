using System.IO;

namespace DotvvmAcademy.DAL.Components
{
    public abstract class ExerciseComponentBase
    {
        public FileInfo Correct { get; set; }

        public FileInfo Incorrect { get; set; }

        public string ValidatorId { get; set; }
    }
}