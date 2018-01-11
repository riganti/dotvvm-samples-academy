using DotvvmAcademy.CommonMark.Segments;
using System.IO;

namespace DotvvmAcademy.DAL.Loadees
{
    public class CSharpExerciseSegment : ExerciseLoadeeBase, IExerciseSegment
    {
        public FileInfo[] Dependencies { get; set; }
    }
}