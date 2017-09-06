using DotvvmAcademy.CommonMark.Components;
using System.IO;

namespace DotvvmAcademy.DAL.Components
{
    public class MvvmExerciseComponent : ICommonMarkComponent
    {
        public ViewExercise View { get; set; }

        public ViewModelExercise ViewModel { get; set; }

        public class ViewExercise : ExerciseComponentBase
        {
            public FileInfo MasterPage { get; set; }
        }

        public class ViewModelExercise : ExerciseComponentBase
        {
            public FileInfo[] Dependencies { get; set; }
        }
    }
}