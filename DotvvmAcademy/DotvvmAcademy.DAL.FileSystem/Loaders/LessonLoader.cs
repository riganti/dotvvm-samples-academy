using DotvvmAcademy.DAL.Base.Entities;
using DotvvmAcademy.DAL.Base.FileSystem;
using DotvvmAcademy.DAL.FileSystem.Services;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DotvvmAcademy.DAL.FileSystem.Loaders
{
    public class LessonLoader : ILoader<ILesson>
    {
        private readonly LessonsJsonDeserializer deserializer;
        private readonly ContentDirectoryEnvironment environment;

        public LessonLoader(ContentDirectoryEnvironment environment, LessonsJsonDeserializer deserializer)
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

        public ILesson LoadAll(IIndexItem<ILesson> item)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<ILesson> Load()
        {
            throw new System.NotImplementedException();
        }
    }
}