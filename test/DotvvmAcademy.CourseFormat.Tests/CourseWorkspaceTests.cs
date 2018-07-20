using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace DotvvmAcademy.CourseFormat.Tests
{
    [TestClass]
    public class CourseWorkspaceTests
    {
        [TestMethod]
        public async Task BasicLoadingTest()
        {
            var workspace = new CourseWorkspace("../../../../../sample/sample_course");
            var variant = await workspace.Load<CourseVariant>("/en");
            var lesson = await workspace.Load<Lesson>("/en/calculator");
            var step = await workspace.Load<Step>("/en/calculator/10_a_classic");
            var codeTask = await workspace.Load<CodeTask>("/en/calculator/10_a_classic/a_classic.csharp.csx");
        }
    }
}
