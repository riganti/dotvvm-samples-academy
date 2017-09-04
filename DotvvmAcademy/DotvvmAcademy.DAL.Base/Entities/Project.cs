using System.Collections.Generic;
using System.IO;

namespace DotvvmAcademy.DAL.Base.Entities
{
    public class Project : IEntity
    {
        public int Id { get; set; }

        public List<Lesson> ReferencingLessons { get; set; }

        public Stream ZipFile { get; set; }
    }
}