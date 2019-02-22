namespace DotvvmAcademy.CourseFormat
{
    public class StepFrontMatter
    {
        public CodeTaskOptions CodeTask { get; set; }

        public EmbeddedViewOptions EmbeddedView { get; set; }

        public string Solution { get; set; }

        public string Title { get; set; }

        public class CodeTaskOptions
        {
            public string Correct { get; set; }

            public string Default { get; set; }

            public string[] Dependencies { get; set; }

            public string Path { get; set; }
        }

        public class EmbeddedViewOptions
        {
            public string[] Dependencies { get; set; }

            public string Path { get; set; }
        }
    }
}