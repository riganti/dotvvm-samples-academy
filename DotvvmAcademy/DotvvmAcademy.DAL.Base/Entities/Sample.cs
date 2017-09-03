using System.Collections.Generic;

namespace DotvvmAcademy.DAL.Base.Entities
{
    public class Sample : IEntity
    {
        public int Id { get; set; }

        public List<SourcePart> ReferencingSourceParts { get; set; }

        public byte[] Source { get; set; }
    }
}