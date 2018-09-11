using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using Xunit;

namespace DotvvmAcademy.CourseFormat.Tests
{
    public class CourseWorkspaceTests
    {
        [Fact]
        public async Task CourseWorkspace_Loading_NotNull()
        {
            var collection = new ServiceCollection();
            collection.AddCourseFormat("../../../../../sample/sample_course");
            var provider = collection.BuildServiceProvider();
            var workspace = provider.GetRequiredService<CourseWorkspace>();
            var variant = await workspace.Load<Variant>("/content/en");
            Assert.NotNull(variant);
            var lesson = await workspace.Load<Lesson>("/content/en/calculator");
            Assert.NotNull(lesson);
            var step = await workspace.Load<Step>("/content/en/calculator/10_a_classic");
            Assert.NotNull(step);
        }
    }
}