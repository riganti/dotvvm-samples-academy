using DotvvmAcademy.CommonMark.Segments;
using System.IO;

namespace DotvvmAcademy.DAL.Loadees
{
    public class MvvmExerciseSegment : ISegment
    {
        public ViewExercise View { get; set; }

        public ViewModelExercise ViewModel { get; set; }

        public class ViewExercise : ExerciseLoadeeBase
        {
            public FileInfo MasterPage { get; set; }
        }

        public class ViewModelExercise : ExerciseLoadeeBase
        {
            public FileInfo[] Dependencies { get; set; }
        }
    }
}