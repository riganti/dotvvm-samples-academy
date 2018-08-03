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
            var wrapper = new CourseCacheWrapper();
            var environment = new CourseEnvironment(new DirectoryInfo("../../../../../sample/sample_course"));
            var loader = new SourceLoader(environment);
            var workspace = new CourseWorkspace(environment, loader, wrapper);
            var variant = await workspace.Load<CourseVariant>("/content/en");
            var lesson = await workspace.Load<Lesson>("/content/en/calculator");
            var step = await workspace.Load<Step>("/content/en/calculator/10_a_classic");
        }
    }
}
