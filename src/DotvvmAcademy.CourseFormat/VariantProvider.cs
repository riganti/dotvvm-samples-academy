using System.Collections.Immutable;
using System.Threading.Tasks;

namespace DotvvmAcademy.CourseFormat
{
    public class VariantProvider : ISourceProvider<Variant>
    {
        private readonly ICourseEnvironment environment;

        public VariantProvider(ICourseEnvironment environment)
        {
            this.environment = environment;
        }

        public async Task<Variant> Get(string path)
        {
            var lessons = (await environment.GetDirectories(path)).ToImmutableArray();
            return new Variant(path, lessons);
        }
    }
}