using DotVVM.Framework.ViewModel;

namespace DotvvmAcademy.Steps.StepsBases.Interfaces
{
    public interface IStep
    {
        int StepIndex { get; set; }

        [Bind(Direction.ServerToClient)]
        string Description { get; set; }

        [Bind(Direction.ServerToClient)]
        string Title { get; set; }
    }
}