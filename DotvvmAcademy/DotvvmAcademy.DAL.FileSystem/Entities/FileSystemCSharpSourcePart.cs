using DotvvmAcademy.DAL.Base.Entities;
using System.Collections.Generic;

namespace DotvvmAcademy.DAL.FileSystem.Entities
{
    public class FileSystemCSharpSourcePart : FileSystemEntity, ICSharpSourcePart
    {
        public List<ISample> Dependencies { get; set; }

        public ISample Correct { get; set; }

        public ISample Incorrect { get; set; }

        public string ValidatorId { get; set; }
    }
}