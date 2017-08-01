using DotVVM.Framework.ViewModel;

namespace DotvvmAcademy.Steps.StepsBases.Interfaces
{
    public interface IStep
    {
        [Bind(Direction.ServerToClient)]
        string Description { get; set; }

        int StepIndex { get; set; }

        [Bind(Direction.ServerToClient)]
        string Title { get; set; }
    }
}