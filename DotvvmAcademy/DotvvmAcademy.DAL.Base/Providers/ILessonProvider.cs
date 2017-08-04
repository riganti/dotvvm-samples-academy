using DotvvmAcademy.DAL.Base.Entities;
using System.Linq;

namespace DotvvmAcademy.DAL.Base.Providers
{
    public interface ILessonProvider
    {
        Lesson Get(int index, string language);

        IQueryable<Lesson> GetQueryable(int? index = null, string language = null);
    }
}