using System.IO;

namespace DotvvmAcademy.DAL.Loadees
{
    public class MvvmExerciseSegment : IExerciseSegment
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