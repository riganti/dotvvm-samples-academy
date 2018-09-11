using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Threading.Tasks;

namespace DotvvmAcademy.CourseFormat.Tests
{
    [TestClass]
    public class CourseWorkspaceTests
    {
        [TestMethod]
        public async Task BasicLoadingTest()
        {
            var collection = new ServiceCollection();
            collection.AddCourseFormat("../../../../../sample/sample_course");
            var provider = collection.BuildServiceProvider();
            var workspace = provider.GetRequiredService<CourseWorkspace>();
            var variant = await workspace.Load<Variant>("/content/en");
            var lesson = await workspace.Load<Lesson>("/content/en/calculator");
            var step = await workspace.Load<Step>("/content/en/calculator/10_a_classic");
        }
    }
}
