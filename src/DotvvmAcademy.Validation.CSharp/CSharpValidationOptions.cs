using Microsoft.Extensions.Options;

namespace DotvvmAcademy.Validation.CSharp
{
    public class CSharpValidationOptions : IOptions<CSharpValidationOptions>
    {
        public bool IncludeCompilerDiagnostics { get; set; } = true;

        CSharpValidationOptions IOptions<CSharpValidationOptions>.Value => this;
    }
}