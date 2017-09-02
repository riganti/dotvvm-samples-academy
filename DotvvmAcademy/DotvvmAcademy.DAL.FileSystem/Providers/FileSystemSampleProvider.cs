using DotvvmAcademy.DAL.Base.Models;
using DotvvmAcademy.DAL.Base.Providers;
using System;
using System.IO;
using System.Linq;

namespace DotvvmAcademy.DAL.FileSystem.Providers
{
    public class FileSystemSampleProvider : FileSystemFileProvider, ISampleProvider
    {
        private readonly ILessonProvider lessonProvider;

        public FileSystemSampleProvider(string applicationRoot, ILessonProvider lessonProvider) : base(applicationRoot)
        {
            this.lessonProvider = lessonProvider;
        }

        public string Get(SampleIdentifier identifier)
        {
            var lesson = lessonProvider.Get(new LessonIdentifier(identifier.LessonId, identifier.Language));
            var stepPath = lesson.StepPaths[identifier.StepIndex];
            return GetFile(Path.Combine(lesson.Path, stepPath, identifier.Path));
        }

        public IQueryable<SampleIdentifier> GetQueryable(SampleFilter filter)
        {
            throw new NotImplementedException();
        }
    }
}