using Microsoft.Extensions.Options;
using System.Collections.Immutable;

namespace DotvvmAcademy.Validation.Dothtml
{
    public class DothtmlValidationOptions : IOptions<DothtmlValidationOptions>
    {
        public ImmutableArray<string> CSharpSources { get; set; }

        public string FileName { get; set; } = "View.dothtml";

        public bool IncludeCompilerDiagnostics { get; set; } = true;

        DothtmlValidationOptions IOptions<DothtmlValidationOptions>.Value => this;
    }
}