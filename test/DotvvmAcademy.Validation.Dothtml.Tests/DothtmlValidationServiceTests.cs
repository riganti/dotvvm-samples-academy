using DotvvmAcademy.Validation.Dothtml.Unit;
using System.Threading.Tasks;
using Xunit;

namespace DotvvmAcademy.Validation.Dothtml.Tests
{
    public class DothtmlValidationServiceTests
    {
        [Fact]
        public async Task DothtmlValidation_DefaultService_CreateErrors()
        {
            var unit = new DothtmlUnit();
            unit.GetControl("/html");
            var service = new DothtmlValidationService();
            var source = new DothtmlSourceCode("@viewModel System.Object", "Test.dothtml", true);
            var diagnostics = await service.Validate(unit.GetConstraints(), new[] { source });
            Assert.NotEmpty(diagnostics);
        }
    }
}