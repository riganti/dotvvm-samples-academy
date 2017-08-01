namespace DotvvmAcademy.Steps.StepsBases.Interfaces
{
    public interface ICodeStepData : IStep
    {
        string FinalCode { get; set; }

        string ShadowBoxDescription { get; set; }

        string StartupCode { get; set; }
    }
}