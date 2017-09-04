using DotvvmAcademy.DAL.Base.Entities;
using System;
using System.Collections.Generic;

namespace DotvvmAcademy.DAL.FileSystem.Entities
{
    public class FileSystemLesson : FileSystemEntity, ILesson
    {
        string ILesson.Annotation => throw new NotImplementedException();

        string ILesson.ImageUrl => throw new NotImplementedException();

        bool ILesson.IsReady => throw new NotImplementedException();

        string ILesson.Language => throw new NotImplementedException();

        string ILesson.Moniker => throw new NotImplementedException();

        string ILesson.Name => throw new NotImplementedException();

        IProject ILesson.Project => throw new NotImplementedException();

        List<IStep> ILesson.Steps => throw new NotImplementedException();

        IValidatorAssembly ILesson.ValidatorAssembly => throw new NotImplementedException();
    }
}