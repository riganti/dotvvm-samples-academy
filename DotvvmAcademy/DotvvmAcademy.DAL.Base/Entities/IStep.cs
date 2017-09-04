using System.Collections.Generic;

namespace DotvvmAcademy.DAL.Base.Entities
{
    public interface IStep : IEntity
    {
        List<ILesson> ReferencingLessons { get; }

        List<ISourcePart> SourceParts { get; }
    }
}