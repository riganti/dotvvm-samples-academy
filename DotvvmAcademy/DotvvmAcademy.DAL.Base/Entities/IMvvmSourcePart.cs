using System.Collections.Generic;

namespace DotvvmAcademy.DAL.Base.Entities
{
    public interface IMvvmSourcePart : ISourcePart
    {
        ISample CorrectView { get; }

        ISample CorrectViewModel { get; }

        ISample IncorrectView { get; }

        ISample IncorrectViewModel { get; }

        ISample MasterPage { get; }

        List<ISample> ViewModelDependencies { get; }

        string ViewModelValidatorId { get; }

        string ViewValidatorId { get; }
    }
}