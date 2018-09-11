using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace DotvvmAcademy.CourseFormat
{
    public interface ICourseEnvironment
    {
        Task<bool> Exists(string path);

        Task<IEnumerable<string>> GetDirectories(string path);

        Task<IEnumerable<string>> GetFiles(string path);

        Stream OpenRead(string path);
    }
}