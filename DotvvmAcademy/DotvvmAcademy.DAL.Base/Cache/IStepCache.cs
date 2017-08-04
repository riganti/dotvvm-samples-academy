using DotvvmAcademy.DAL.Base.Entities;

namespace DotvvmAcademy.DAL.Base.Cache
{
    public interface IStepCache : ICache<StepIdentifier, string>
    {
        void Add(string step, Lesson lesson, int stepIndex);

        void Add(string step, LessonIdentifier lessonIdentifier, int stepIndex);
    }
}