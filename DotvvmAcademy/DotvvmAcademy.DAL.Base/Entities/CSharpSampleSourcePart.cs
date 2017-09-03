using System.Collections.Generic;

namespace DotvvmAcademy.DAL.Base.Entities
{
    public class CSharpSampleSourcePart : BasicSampleSourcePart
    {
        public List<Sample> Dependencies { get; set; }
    }
}