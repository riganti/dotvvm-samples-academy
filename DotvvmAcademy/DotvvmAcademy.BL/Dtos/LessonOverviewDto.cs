namespace DotvvmAcademy.BL.Dtos
{
    public sealed class LessonOverviewDto
    {
        public string Annotation { get; internal set; }

        public string ImageUrl { get; internal set; }

        public string Language { get; internal set; }

        public string Moniker { get; internal set; }

        public string Name { get; internal set; }

        public int StepCount { get; internal set; }
    }
}