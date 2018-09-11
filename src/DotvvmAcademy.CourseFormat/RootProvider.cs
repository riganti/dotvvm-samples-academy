using System;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;

namespace DotvvmAcademy.CourseFormat
{
    public class RootProvider : ISourceProvider<Root>
    {
        private readonly ICourseEnvironment environment;

        public RootProvider(ICourseEnvironment environment)
        {
            this.environment = environment;
        }

        public async Task<Root> Get(string path)
        {
            var directories = await environment.GetDirectories("/");
            var hasContent = directories.Contains("content");
            var variants = hasContent ? (await environment.GetDirectories("/content")).ToImmutableArray() : ImmutableArray.Create<string>();
            return new Root(
                variants: variants,
                hasContent: hasContent,
                hasResources: directories.Contains("resources"));
        }
    }
}