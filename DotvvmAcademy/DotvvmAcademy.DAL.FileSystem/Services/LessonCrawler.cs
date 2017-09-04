using DotvvmAcademy.DAL.Base.FileSystem;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DotvvmAcademy.DAL.FileSystem.Services
{
    public class LessonCrawler
    {
        private readonly LessonsJsonDeserializer deserializer;
        private readonly ContentDirectoryEnvironment environment;

        public LessonCrawler(ContentDirectoryEnvironment environment, LessonsJsonDeserializer deserializer)
        {
            this.environment = environment;
            this.deserializer = deserializer;
        }

        public async Task<List<LessonConfig>> GetLessons()
        {
            var directories = Directory.EnumerateDirectories(environment.ContentDirectoryPath);
            List<LessonConfig> lessons = new List<LessonConfig>();
            foreach (var directory in directories)
            {
                lessons.AddRange(await GetLessons(directory));
            }

            return lessons;
        }

        public async Task<List<LessonConfig>> GetLessons(string directory)
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