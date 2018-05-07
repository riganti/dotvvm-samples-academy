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
            var variant = await workspace.LoadVariant(workspace.Variants[0]);
            var lesson = await workspace.LoadLesson(variant.Lessons[0]);
            var step0 = await workspace.LoadStep(lesson.Steps[0]);
            var step1 = await workspace.LoadStep(lesson.Steps[1]);
        }
    }
}
