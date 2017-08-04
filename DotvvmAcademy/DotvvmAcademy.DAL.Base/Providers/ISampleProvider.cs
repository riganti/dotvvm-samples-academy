using DotvvmAcademy.DAL.Base.Entities;
using System.Collections.Generic;
using System.Linq;

namespace DotvvmAcademy.DAL.Base.Providers
{
    public interface ISampleProvider
    {
        string Get(Lesson lesson, string path);

        IQueryable<string> GetQueryable(Lesson lesson, IEnumerable<string> paths);
    }
}
