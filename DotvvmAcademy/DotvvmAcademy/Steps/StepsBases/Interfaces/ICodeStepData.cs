namespace DotvvmAcademy.Steps.StepsBases.Interfaces
{
    public interface ICodeStepData : IStep
    {
        string StartupCode { get; set; }
        string FinalCode { get; set; }
        string ShadowBoxDescription { get; set; }
    }
}