using DotvvmAcademy.DAL.Base.Models;

namespace DotvvmAcademy.DAL.Base.Providers
{
    public interface ILessonProvider : IProvider<LessonIdentifier, LessonFilter, Lesson>
    {
    }
}