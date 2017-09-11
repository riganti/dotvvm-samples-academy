using DotvvmAcademy.DAL.Loadees;
using DotvvmAcademy.DAL.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotvvmAcademy.DAL.Loaders
{
    public class LessonLoader : ILoader
    {
        private readonly LessonConfigDeserializer deserializer;
        private readonly ContentDirectoryEnvironment environment;

        public LessonLoader(LessonConfigDeserializer deserializer, ContentDirectoryEnvironment environment)
        {
            this.deserializer = deserializer;
            this.environment = environment;
        }

        public async Task<IEnumerable<LessonConfigLoadee>> LoadLessons(string moniker = null, string language = null)
        {
            var directories = environment.LessonsDirectory.EnumerateDirectories();
            if(moniker != null)
            {
                directories = directories.Where(d => d.Name == moniker);
            }
            var tasks = directories.Select(directory => LoadLessons(directory, language));
            return (await Task.WhenAll(tasks)).SelectMany(a => a);
        }

        public Task<LessonConfigLoadee[]> LoadLessons(DirectoryInfo directory, string language = null)
        {
            var configs = directory.EnumerateFiles(GetConfigSearchPattern(language));
            var tasks = configs.Select(config => LoadLesson(config));
            return Task.WhenAll(tasks);
        }

        public async Task<LessonConfigLoadee> LoadLesson(FileInfo file)
        {
            if (file.Exists)
            {
                var lesson = await deserializer.Deserialize(file);
                lesson.File = file;
                lesson.Moniker = file.Directory.Name;
                lesson.Language = GetLanguage(file.Name);
                return lesson;
            }
            return null;
        }

        private string GetLanguage(string fileName)
        {
            var firstIndex = fileName.IndexOf('.');
            var lastIndex = fileName.LastIndexOf('.');
            return fileName.Substring(firstIndex + 1, lastIndex - (firstIndex + 1));
        }

        private string GetConfigSearchPattern(string language)
        {
            var sb = new StringBuilder();
            sb.Append(ContentConstants.LessonConfig);
            sb.Append('.');
            sb.Append(language ?? "*");
            sb.Append('.');
            sb.Append(ContentConstants.LessonConfigFormat);
            return sb.ToString();
        }
    }
}