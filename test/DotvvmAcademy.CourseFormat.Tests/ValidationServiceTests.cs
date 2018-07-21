using DotvvmAcademy.Validation.CSharp;
using DotvvmAcademy.Validation.Dothtml;
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
            var codeTask = await workspace.Load<CodeTask>("/en/calculator/10_a_classic/a_classic.csharp.csx");
            var csharpService = new CSharpValidationService();
            var dothtmlService = new DothtmlValidationService();
            var validator = new CodeTaskValidator(workspace, csharpService, dothtmlService);
            var unit = await validator.GetUnit(codeTask);
            var diagnostics = await validator.Validate(unit, @"namespace SampleCourse
{
    public class CalculatorViewModel
    {
    }
}");
        }

        [TestMethod]
        public async Task LessBasicValidationTest()
        {
            var workspace = new CourseWorkspace("../../../../../sample/sample_course");
            var codeTask = await workspace.Load<CodeTask>("/en/calculator/50_methods/methods.csharp.csx");
            var csharpService = new CSharpValidationService();
            var dothtmlService = new DothtmlValidationService();
            var validator = new CodeTaskValidator(workspace, csharpService, dothtmlService);
            var unit = await validator.GetUnit(codeTask);
            var diagnostics = await validator.Validate(unit,
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