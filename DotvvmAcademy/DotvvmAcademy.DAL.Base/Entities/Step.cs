using System.Collections.Generic;

namespace DotvvmAcademy.DAL.Base.Entities
{
    public class Step : IEntity
    {
        public int Id { get; set; }

        public List<Lesson> ReferencingLessons { get; set; }

        public List<SourcePart> SourceParts { get; set; }
    }
}