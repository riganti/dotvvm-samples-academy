using DotvvmAcademy.DAL.Base.Models;
using DotvvmAcademy.DAL.Base.Providers;
using DotvvmAcademy.DAL.Base.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DotvvmAcademy.DAL.FileSystem.Providers
{
    public class FileSystemLessonProvider : FileSystemFileProvider, ILessonProvider
    {
        private readonly string lessonsDirectoryAbsolutePath;
        private readonly string lessonsDirectoryRelativePath = $"./{ContentConstants.ContentDirectoryName}/{ContentConstants.LessonsDirectoryName}";
        private ILessonDeserializer deserializer;

        public FileSystemLessonProvider(string applicationRoot, ILessonDeserializer deserializer) : base(applicationRoot)
        {
            lessonsDirectoryAbsolutePath = Path.Combine(RootPath, lessonsDirectoryRelativePath);
            this.deserializer = deserializer;
        }

        public Lesson Get(LessonIdentifier identifier)
        {
            var lessons = GetLessons(identifier.LessonId, identifier.Language);
            return lessons.Single();
        }

        public IQueryable<LessonIdentifier> GetQueryable(LessonFilter filter)
        {
            var lessons = GetLessons(filter.LessonId, filter.Language);
            return lessons
                .Select(l => new LessonIdentifier(l.LessonId, l.Language))
                .OrderBy(i => i.LessonId)
                .AsQueryable();
        }

        private IEnumerable<Lesson> GetLessons(string lessonId, string language)
        {
            if (!Directory.Exists(lessonsDirectoryAbsolutePath))
            {
                throw new InvalidOperationException($"No lessons can be found because the '{ContentConstants.LessonsDirectoryName}' " +
                    $"directory doesn't exist in the provided root directory.");
            }

            foreach (var directory in Directory.EnumerateDirectories(lessonsDirectoryAbsolutePath))
            {
                if (TryGetLessonsFromDirectory(directory, out var lessons))
                {
                    if (lessonId != null)
                    {
                        lessons = lessons.Where(l => l.LessonId.Equals(lessonId));
                    }

                    if(language != null)
                    {
                        lessons = lessons.Where(l => l.Language.Equals(language));
                    }

                    foreach (var lesson in lessons)
                    {
                        yield return lesson;
                    }
                }
            }
        }

        private bool TryGetLessonsFromDirectory(string directory, out IEnumerable<Lesson> lessons)
        {
            string lessonsJsonPath = $"{directory}/{ContentConstants.LessonConfigurationFileName}";
            lessonsJsonPath = Path.Combine(lessonsDirectoryAbsolutePath, lessonsJsonPath);
            if (!File.Exists(lessonsJsonPath))
            {
                lessons = null;
                return false;
            }

            lessons = deserializer.Deserialize(GetFile(lessonsJsonPath));
            foreach (var lesson in lessons)
            {

                lesson.Path = lessonsJsonPath;
            }
            return true;
        }
    }
}