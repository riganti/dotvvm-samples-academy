using DotvvmAcademy.Validation.CSharp.Unit;
using System.Threading.Tasks;
using Xunit;

namespace DotvvmAcademy.Validation.CSharp.Tests
{
    public class CSharpValidationServiceTests
    {
        [Fact]
        public async Task CSharpValidation_DefaultService_CreateErrors()
        {
            var unit = new CSharpUnit();
            unit.GetMethod("Test::Off");
            var service = new CSharpValidationService();
            var source = new CSharpSourceCode("public class Test {}", "Test.cs", true);
            var diagnostics = await service.Validate(unit.GetConstraints(), new[] { source });
            Assert.NotEmpty(diagnostics);
        }
    }
}