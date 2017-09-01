using DotvvmAcademy.DAL.Base;
using DotvvmAcademy.DAL.Base.Entities;
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
        private readonly string rootLessonsDirectoryAbsolutePath;
        private readonly string rootLessonsDirectoryRelativePath = $"./{ContentConstants.ContentDirectoryName}/{ContentConstants.LessonsDirectoryName}";
        private ILessonDeserializer deserializer;

        public FileSystemLessonProvider(string applicationRoot, ILessonDeserializer deserializer) : base(applicationRoot)
        {
            rootLessonsDirectoryAbsolutePath = Path.Combine(RootPath, rootLessonsDirectoryRelativePath);
            this.deserializer = deserializer;
        }

        public Lesson Get(int index, string language) => GetQueryable(index, language).Single();

        public IQueryable<Lesson> GetQueryable(int? index = null, string language = null)
        {
            return GetLessons(index, language).OrderBy(l => l.Index).AsQueryable();
        }

        private IEnumerable<Lesson> GetLessons(int? index, string language)
        {
            if (!Directory.Exists(rootLessonsDirectoryAbsolutePath))
            {
                throw new InvalidOperationException($"No lessons can be found because the '{ContentConstants.LessonsDirectoryName}' " +
                    $"directory doesn't exist in the provided root directory.");
            }
            var lessonDirectories = Directory.EnumerateDirectories(rootLessonsDirectoryAbsolutePath);
            foreach (var lessonDirectory in lessonDirectories)
            {
                string configRelativePath = $"{lessonDirectory}/{ContentConstants.LessonConfigurationFileName}";
                string configAbsolutePath = Path.Combine(rootLessonsDirectoryAbsolutePath, configRelativePath);
                if (!File.Exists(configAbsolutePath))
                {
                    continue;
                }

                var rawFile = GetFile(configAbsolutePath);
                var deserializedLessons = deserializer.Deserialize(rawFile);
                if (language != null)
                {
                    deserializedLessons = deserializedLessons.Where(l => l.Language == language);
                }

                if (index != null)
                {
                    deserializedLessons = deserializedLessons.Where(l => l.Index == index);
                }

                foreach (var lesson in deserializedLessons)
                {
                    lesson.ConfigPath = configAbsolutePath;
                    lesson.DirectoryPath = configAbsolutePath.Substring(0, configAbsolutePath.LastIndexOf('/'));
                    yield return lesson;
                }
            }
        }
    }
}