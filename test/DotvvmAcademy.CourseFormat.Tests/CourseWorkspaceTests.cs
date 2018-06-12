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
            var variant = await workspace.LoadVariant("/en");
            var lesson = await workspace.LoadLesson("/en/introduction");
            var step = await workspace.LoadStep("/en/introduction/20_first_task");
            var codeTask = await workspace.LoadCodeTask("/en/introduction/20_first_task");
        }
    }
}
