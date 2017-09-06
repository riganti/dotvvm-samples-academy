using DotvvmAcademy.CommonMark.Components;
using System.IO;

namespace DotvvmAcademy.DAL.Components
{
    public class CSharpExerciseComponent : ExerciseComponentBase, ICommonMarkComponent
    {
        public FileInfo[] Dependencies { get; set; }
    }
}