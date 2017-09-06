using DotvvmAcademy.CommonMark.Segments;
using System.IO;

namespace DotvvmAcademy.DAL.Loadees
{
    public class CSharpExerciseSegment : ExerciseLoadeeBase, ISegment
    {
        public FileInfo[] Dependencies { get; set; }
    }
}