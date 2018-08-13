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

        [Fact]
        public void TemplateSetters_ItemTemplate_Exist()
        {
            var tree = GetValidationTree(@"
@viewModel System.Object

<dot:Repeater DataSource=""{value: this}"">
<p>{{value: this}}</p>
</dot:Repeater>");
        }
    }
}