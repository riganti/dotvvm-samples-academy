using Microsoft.CodeAnalysis;

namespace DotvvmAcademy.Validation.CSharp
{
    public class RoslynValidationDiagnosticLocation : ValidationDiagnosticLocation
    {
        private readonly Location location;

        public RoslynValidationDiagnosticLocation(Location location)
            : base(location.SourceSpan.Start, location.SourceSpan.Length)
        {
            this.location = location;
        }

        public override object GetNativeObject()
        {
            return location;
        }
    }
}