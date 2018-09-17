using System.Collections.Generic;

namespace DotvvmAcademy.CourseFormat
{
    public class StepFrontMatter
    {
        public string CodeTask { get; set; }

        public EmbeddedViewOptions EmbeddedView { get; set; }

        public string Solution { get; set; }

        public string Title { get; set; }

        public class EmbeddedViewOptions
        {
            public List<string> Dependencies { get; set; }

            public string Path { get; set; }
        }
    }
}