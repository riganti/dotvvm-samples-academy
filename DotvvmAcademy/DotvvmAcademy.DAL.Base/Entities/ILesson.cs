using System.Collections.Generic;

namespace DotvvmAcademy.DAL.Base.Entities
{
    public interface ILesson : IEntity
    {
        string Annotation { get; }

        string ImageUrl { get; }

        bool IsReady { get; }

        string Language { get; }

        string Moniker { get; }

        string Name { get; }

        IProject Project { get; }

        List<IStep> Steps { get; }

        IValidatorAssembly ValidatorAssembly { get;}
    }
}