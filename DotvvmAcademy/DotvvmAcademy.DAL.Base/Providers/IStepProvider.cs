using DotvvmAcademy.DAL.Base.Models;
using System.Linq;

namespace DotvvmAcademy.DAL.Base.Providers
{
    public interface IStepProvider
    {
        string Get(Lesson lesson, int index);

        IQueryable<string> GetQueryable(Lesson lesson);
    }
}
