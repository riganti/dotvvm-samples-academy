using DotvvmAcademy.CommonMark.Components;
using System.IO;

namespace DotvvmAcademy.DAL.Components
{
    public class DothtmlExerciseComponent : ExerciseComponentBase, ICommonMarkComponent
    {
        public FileInfo MasterPage { get; set; }

        public FileInfo ViewModel { get; set; }
    }
}