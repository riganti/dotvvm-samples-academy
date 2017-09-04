using DotvvmAcademy.DAL.Base.Entities;
using DotvvmAcademy.DAL.Base.FileSystem;
using DotvvmAcademy.DAL.FileSystem.Index.Items;
using DotvvmAcademy.DAL.FileSystem.Services;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DotvvmAcademy.DAL.FileSystem.Loaders
{
    public class LessonLoader : ILoader<ILesson, LessonItem>
    {
        private readonly LessonConfigDeserializer deserializer;
        private readonly ContentDirectoryEnvironment environment;

        public LessonLoader(ContentDirectoryEnvironment environment, LessonConfigDeserializer deserializer)
        {
            this.environment = environment;
            this.deserializer = deserializer;
        }

        public Task<ILesson> Load(LessonItem item)
        {
        }

        public Task<IEnumerable<ILesson>> LoadAll()
        {
            throw new System.NotImplementedException();
        }

        private async Task<List<LessonConfig>> GetLessons()
        {
            var directories = Directory.EnumerateDirectories(environment.ContentDirectoryPath);
            List<LessonConfig> lessons = new List<LessonConfig>();
            foreach (var directory in directories)
            {
                lessons.AddRange(await GetLessons(directory));
            }

            return lessons;
        }

        private async Task<List<LessonConfig>> GetLessons(string directory)
        {
            var potentialLessonsConfig = Path.Combine(directory, ContentConstants.LessonConfig);
            if (File.Exists(potentialLessonsConfig))
            {
                var lessons = (await deserializer.Deserialize(File.OpenText(potentialLessonsConfig))).ToList();
                for (int i = 0; i < lessons.Count; i++)
                {
                    var lesson = lessons[i];
                    lesson.Path = potentialLessonsConfig;
                    lesson.FileIndex = i;
                }
                return lessons;
            }

            return Enumerable.Empty<LessonConfig>().ToList();
        }
    }
}