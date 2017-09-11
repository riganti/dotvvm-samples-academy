using DotvvmAcademy.CommonMark.Segments;
using System.IO;

namespace DotvvmAcademy.DAL.Loadees
{
    public class StepLoadee
    {
        public FileInfo File { get; set; }

        public ISegment[] Segments { get; set; }
    }
}