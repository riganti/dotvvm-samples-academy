namespace DotvvmAcademy.BL.Dtos
{
    public sealed class HtmlStepPartDto : IStepPartDto
    {
        public string Source { get; internal set; }

        public StepDto Step { get; set; }
    }
}