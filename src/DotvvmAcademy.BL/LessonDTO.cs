using System.Collections.Generic;

namespace DotvvmAcademy.BL
{
    public class LessonDTO
    {
        public string Annotation { get; set; }

        public string ImageUrl { get; set; }

        public bool IsFinished { get; set; }

        public string CurrentStep { get; set; }

        public string Moniker { get; set; }

        public string Name { get; set; }

        public List<string> Steps { get; set; }
    }
}