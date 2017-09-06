using DotvvmAcademy.CommonMark.Components;
using System.IO;

namespace DotvvmAcademy.DAL.Components
{
    internal class MvvmExerciseComponent : ICommonMarkComponent
    {
        public ViewExercise View { get; set; }

        public ViewModelExercise ViewModel { get; set; }

        internal class ViewExercise : ExerciseComponentBase
        {
            public FileInfo MasterPage { get; set; }
        }

        internal class ViewModelExercise : ExerciseComponentBase
        {
            public FileInfo[] Dependencies { get; set; }
        }
    }
}