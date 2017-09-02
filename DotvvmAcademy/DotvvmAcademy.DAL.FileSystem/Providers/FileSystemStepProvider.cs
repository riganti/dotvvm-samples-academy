using DotvvmAcademy.DAL.Base.Models;
using DotvvmAcademy.DAL.Base.Providers;
using System;
using System.IO;
using System.Linq;

namespace DotvvmAcademy.DAL.FileSystem.Providers
{
    public class FileSystemStepProvider : FileSystemFileProvider, IStepProvider
    {
        private readonly ILessonProvider lessonProvider;

        public FileSystemStepProvider(string rootPath, ILessonProvider lessonProvider) : base(rootPath)
        {
            this.lessonProvider = lessonProvider;
        }

        public string Get(StepIdentifier identifier)
        {
            var lesson = lessonProvider.Get(new LessonIdentifier(identifier.LessonId, identifier.Language));
            return GetFile(Path.Combine(lesson.Path, lesson.StepPaths[identifier.StepIndex]));
        }

        public IQueryable<StepIdentifier> GetQueryable(StepFilter filter)
        {
            throw new NotImplementedException();
        }
    }
}