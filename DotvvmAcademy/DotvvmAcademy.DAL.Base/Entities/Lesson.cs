using System.Collections.Generic;

namespace DotvvmAcademy.DAL.Base.Entities
{
    public class Lesson : IEntity
    {
        public string Annotation { get; set; }

        public int Id { get; set; }

        public string ImageUrl { get; set; }

        public bool IsReady { get; set; }

        public string Language { get; set; }

        public string Moniker { get; set; }

        public string Name { get; set; }

        public string Path { get; set; }

        public Project Project { get; set; }

        public List<Step> Steps { get; set; }

        public ValidatorAssembly ValidatorAssembly { get; set; }
    }
}