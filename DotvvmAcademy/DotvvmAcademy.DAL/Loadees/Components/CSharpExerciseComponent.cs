using DotvvmAcademy.CommonMark.Components;
using System.IO;

namespace DotvvmAcademy.DAL.Components
{
    internal class CSharpExerciseComponent : ExerciseComponentBase, ICommonMarkComponent
    {
        public FileInfo[] Dependencies { get; set; }
    }
}