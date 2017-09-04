using DotvvmAcademy.DAL.Base.Entities;
using DotvvmAcademy.DAL.FileSystem.Index.Items;
using DotvvmAcademy.DAL.FileSystem.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using DotvvmAcademy.DAL.FileSystem.Loaders;

namespace DotvvmAcademy.DAL.FileSystem.Index
{
    public class LessonIndex : IIndex<ILesson>
    {
        private readonly LessonLoader loader;

        public LessonIndex(LessonLoader loader)
        {
            this.loader = loader;
        }

        public List<LessonItem> Items { get; set; }

        ILoader<ILesson> IIndex<ILesson>.Loader => loader;

        IEnumerable<IIndexItem<ILesson>> IIndex<ILesson>.Items => Items;

        public async Task Create()
        {
            int currentId = 1;
            Items = new List<LessonItem>();
            var lessons = await crawler.GetLessons();
            foreach (var lesson in lessons)
            {
                var item = new LessonItem { Id = currentId, Path = lesson.Path, FileIndex = lesson.FileIndex };
                Items.Add(item);
                currentId++;
            }
        }

        Task IIndex<ILesson>.Create()
        {
            throw new System.NotImplementedException();
        }
    }
}