using System.IO;

namespace DotvvmAcademy.DAL.Loadees
{
    public class DothtmlExerciseSegment : ExerciseLoadeeBase, IExerciseSegment
    {
        public FileInfo MasterPage { get; set; }

        public FileInfo ViewModel { get; set; }
    }
}