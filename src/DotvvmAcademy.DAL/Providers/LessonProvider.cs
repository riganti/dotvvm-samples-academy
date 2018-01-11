using AutoMapper;
using DotvvmAcademy.DAL.Entities;
using DotvvmAcademy.DAL.Loadees;
using DotvvmAcademy.DAL.Loaders;
using DotvvmAcademy.DAL.Services;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DotvvmAcademy.DAL.Providers
{
    public class LessonProvider : IEntityProvider<Lesson>
    {
        private readonly ContentDirectoryEnvironment environment;
        private readonly LessonLoader lessonLoader;

        public LessonProvider(LessonLoader lessonLoader, ContentDirectoryEnvironment environment)
        {
            this.lessonLoader = lessonLoader;
            this.environment = environment;
        }

        public async Task<Lesson> Get(string path)
        {
            var file = new FileInfo(Path.Combine(environment.ContentDirectory.FullName, path));
            var config = await lessonLoader.LoadLesson(file);
            return Mapper.Map<LessonConfigLoadee, Lesson>(config);
        }

        public async Task<Lesson> Get(string moniker, string language)
        {
            var configs = (await lessonLoader.LoadLessons(moniker, language)).ToList();

            return Mapper.Map<LessonConfigLoadee, Lesson>(configs.SingleOrDefault());
        }

        public async Task<IEnumerable<Lesson>> GetMany(string moniker = null, string language = null)
        {
            var configs = await lessonLoader.LoadLessons(moniker, language);
            return configs.Select(c => Mapper.Map<LessonConfigLoadee, Lesson>(c));
        }
    }
}