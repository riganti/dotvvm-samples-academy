using DotvvmAcademy.DAL.Base.Entities;

namespace DotvvmAcademy.DAL.Base.Cache
{
    public interface ILessonCache : ICache<LessonIdentifier, Lesson>
    {
        void Add(Lesson lesson);
    }
}