using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
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
            var codeTask = await workspace.LoadCodeTask("/en/introduction/20_first_task");
            var service = new ValidationService();
            var diagnostics = await service.Validate(codeTask, @"namespace SampleCourse
{
    public class Test
    {
    }
}");
        }
    }
}
