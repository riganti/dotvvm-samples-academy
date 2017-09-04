using DotvvmAcademy.DAL.FileSystem.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DotvvmAcademy.DAL.FileSystem.EntityIndex
{
    public class LessonIndex : IEntityIndex<LessonIndexEntity>
    {
        private readonly LessonCrawler crawler;

        public LessonIndex(LessonCrawler crawler)
        {
            this.crawler = crawler;
        }

        public List<LessonIndexEntity> Items { get; set; }

        public async Task Create()
        {
            int currentId = 1;
            Items = new List<LessonIndexEntity>();
            var lessons = await crawler.GetLessons();
            foreach (var lesson in lessons)
            {
                var item = new LessonIndexEntity { Id = currentId, Path = lesson.Path, FileIndex = lesson.FileIndex};
                Items.Add(item);
                currentId++;
            }
        }
    }
}