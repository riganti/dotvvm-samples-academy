using DotvvmAcademy.CommonMark.Segments;
using System.IO;

namespace DotvvmAcademy.DAL.Loadees
{
    public class DothtmlExerciseSegment : ExerciseLoadeeBase, ISegment
    {
        public FileInfo MasterPage { get; set; }

        public FileInfo ViewModel { get; set; }
    }
}