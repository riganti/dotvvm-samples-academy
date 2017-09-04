using System.Collections.Generic;

namespace DotvvmAcademy.DAL.Base.Entities
{
    public interface ICSharpSourcePart : IBasicSampleSourcePart
    {
        List<ISample> Dependencies { get; }
    }
}