using DotvvmAcademy.Steps.Validation.Interfaces;

namespace DotvvmAcademy.Steps.StepsBases
{
    public interface ICodeStep<T> : IStep, ICodeStepData, ICodeStepCode
        where T : ILessonValidationObject
    {
        T Validator { get; set; }
    }

    public interface ICodeStepData : IStep
    {
        string StartupCode { get; set; }
        string FinalCode { get; set; }
        string ShadowBoxDescription { get; set; }
    }

    public interface ICodeStepCode
    {
        void ResetCode();
        void ShowCorrectCode();
    }
}