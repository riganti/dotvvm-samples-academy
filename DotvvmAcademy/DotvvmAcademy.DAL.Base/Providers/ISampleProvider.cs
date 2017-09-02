using DotvvmAcademy.DAL.Base.Models;
using System.Collections.Generic;
using System.Linq;

namespace DotvvmAcademy.DAL.Base.Providers
{
    public interface ISampleProvider
    {
        string Get(Lesson lesson, int stepIndex, string path);

        IQueryable<string> GetQueryable(Lesson lesson, int stepIndex, IEnumerable<string> paths);
    }
}
