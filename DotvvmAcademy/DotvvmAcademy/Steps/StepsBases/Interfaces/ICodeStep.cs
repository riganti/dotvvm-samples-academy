using DotvvmAcademy.Steps.Validation.Interfaces;

namespace DotvvmAcademy.Steps.StepsBases.Interfaces
{
    public interface ICodeStep<T> : ICodeStepData, ICodeStepCode
        where T : ILessonValidationObject
    {
        T Validator { get; set; }
    }
}