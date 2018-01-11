using System.IO;

namespace DotvvmAcademy.DAL.Loadees
{
    public class MvvmExerciseSegment : IExerciseSegment
    {
        public ViewExerciseLoadee View { get; set; }

        public ViewModelExerciseLoadee ViewModel { get; set; }

        public class ViewExerciseLoadee : ExerciseLoadeeBase
        {
            public FileInfo MasterPage { get; set; }
        }

        public class ViewModelExerciseLoadee : ExerciseLoadeeBase
        {
            public FileInfo[] Dependencies { get; set; }
        }
    }
}