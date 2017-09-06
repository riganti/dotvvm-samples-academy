namespace DotvvmAcademy.DAL.Entities
{
    public class Lesson : IEntity
    {
        public string Annotation { get; set; }

        public string ImageUrl { get; set; }

        public bool IsReady { get; set; }

        public string Language { get; set; }

        public string Moniker { get; set; }

        public string Name { get; set; }

        public string Path { get; set; }

        public string[] StepPaths { get; set; }
    }
}