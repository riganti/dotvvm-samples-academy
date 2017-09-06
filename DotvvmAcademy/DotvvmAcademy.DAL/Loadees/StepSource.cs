using DotvvmAcademy.CommonMark.Components;
using System.Collections.Generic;
using System.IO;

namespace DotvvmAcademy.DAL.Loadees
{
    public class StepSource
    {
        public FileInfo File { get; set; }

        public List<ICommonMarkComponent> Components { get; set; }
    }
}