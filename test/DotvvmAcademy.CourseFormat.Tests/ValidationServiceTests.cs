using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace DotvvmAcademy.CourseFormat.Tests
{
    [TestClass]
    public class ValidationServiceTests
    {
        [TestMethod]
        public async Task BasicValidationTest()
        {
            var workspace = new CourseWorkspace("../../../../../sample/sample_course");
            var service = new ValidationService();
            var diagnostics = await service.Validate(workspace, workspace.CodeTaskIds["/en/introduction/20_first_task"], @"namespace SampleCourse
{
    public class Test
    {
    }
}");
        }

        [TestMethod]
        public async Task LessBasicValidationTest()
        {
            var workspace = new CourseWorkspace("../../../../../sample/sample_course");
            var service = new ValidationService();
            var diagnostics = await service.Validate(workspace, workspace.CodeTaskIds["/en/introduction/30_second_task"], 
                @"namespace SampleCourse
{
    public class Test
    {
        public int Add(int one, int two)
        {
            while(true) {}
            return 42;
        }
    }
}");
        }
    }
}