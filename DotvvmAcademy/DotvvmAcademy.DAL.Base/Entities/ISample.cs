using System.Collections.Generic;

namespace DotvvmAcademy.DAL.Base.Entities
{
    public interface ISample : IEntity
    {
        List<ISourcePart> ReferencingSourceParts { get; }

        string Source { get; }
    }
}