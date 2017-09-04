using DotvvmAcademy.CommonMark.Components;
using System.Collections.Generic;

namespace DotvvmAcademy.DAL.FileSystem
{
    public class StepSource
    {
        public List<IComponent> Components { get; set; }

        public string Path { get; set; }
    }
}