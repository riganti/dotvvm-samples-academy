using DotvvmAcademy.DAL.Base.Entities;
using Nito.AsyncEx;
using System;
using System.Collections.Generic;

namespace DotvvmAcademy.DAL.FileSystem.Entities
{
    public class FileSystemLesson : FileSystemEntity, ILesson
    {
        string ILesson.Annotation => Config.Task.Result;

        string ILesson.ImageUrl => Config.Value.ImageUrl;

        bool ILesson.IsReady => Config.Value.IsReady;

        string ILesson.Language => Config.Value.Language;

        string ILesson.Moniker => Config.Value.Moniker;

        string ILesson.Name => Config.Value.Name;

        IProject ILesson.Project => throw new NotImplementedException();

        List<IStep> ILesson.Steps => Steps.Value;

        IValidatorAssembly ILesson.ValidatorAssembly => throw new NotImplementedException();

        internal AsyncLazy<LessonConfig> Config { get; set; }

        internal AsyncLazy<List<IStep>> Steps { get; set; }
    }
}