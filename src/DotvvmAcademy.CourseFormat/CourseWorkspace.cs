using System.Collections.Immutable;
using System.Threading.Tasks;

namespace DotvvmAcademy.CourseFormat
{
    public class CourseWorkspace
    {
        public ImmutableArray<ICourse> Courses { get; private set; }

        public string RootDirectory { get; private set; }

        public Task Load(string rootDirectory)
        {
            RootDirectory = rootDirectory;
        }
    }
}