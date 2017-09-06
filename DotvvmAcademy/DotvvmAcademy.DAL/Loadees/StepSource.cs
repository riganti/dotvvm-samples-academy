using DotvvmAcademy.CommonMark.Segments;
using System.Collections.Generic;
using System.IO;

namespace DotvvmAcademy.DAL.Loadees
{
    public class StepSource
    {
        public FileInfo File { get; set; }

        public ISegment[] Source { get; set; }
    }
}