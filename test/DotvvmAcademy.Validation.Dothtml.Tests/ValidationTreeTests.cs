using Xunit;

namespace DotvvmAcademy.Validation.Dothtml.Tests
{
    public class ValidationTreeTests : DothtmlTestBase
    {
        [Fact]
        public void PropertyGroups_ElementHasHtmlAttribute_AttributesGroupFound()
        {
            var tree = GetValidationTree("<meta charset='utf-8'>");
        }
    }
}