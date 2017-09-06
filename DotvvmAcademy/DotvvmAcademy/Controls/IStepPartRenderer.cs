using DotvvmAcademy.BL.Dtos;

namespace DotvvmAcademy.Controls
{
    public interface IStepPartRenderer
    {
        void SetBindings(StepRenderer renderer);

        void SetPart(IStepPartDto part);
    }
}