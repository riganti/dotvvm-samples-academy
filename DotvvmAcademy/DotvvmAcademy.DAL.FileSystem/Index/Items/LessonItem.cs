using DotvvmAcademy.DAL.Base.Entities;

namespace DotvvmAcademy.DAL.FileSystem.Index.Items
{
    public class LessonItem : IndexItemBase<ILesson>
    {
        public int ArrayIndex { get; set; }

        public int StepCount { get; set; }
    }
}