using Microsoft.CodeAnalysis;

namespace DotvvmAcademy.Validation.CSharp
{
    public class RoslynValidationDiagnostic : ValidationDiagnostic
    {
        public RoslynValidationDiagnostic(Diagnostic diagnostic)
        {
            Diagnostic = diagnostic;
            Location = MapLocation(diagnostic.Location);
            Severity = MapSeverity(diagnostic.Severity);
        }

        public Diagnostic Diagnostic { get; }

        public override string Id => Diagnostic.Id;

        public override ValidationDiagnosticLocation Location { get; }

        public override string Message => Diagnostic.GetMessage();

        public override string Name => Diagnostic.Descriptor.Title.ToString();

        public override ValidationDiagnosticSeverity Severity { get; }

        private ValidationDiagnosticLocation MapLocation(Location location)
        {
            if (location.Kind == LocationKind.None)
            {
                return ValidationDiagnosticLocation.Global;
            }
            return new RoslynValidationDiagnosticLocation(location);
        }

        private ValidationDiagnosticSeverity MapSeverity(DiagnosticSeverity severity)
        {
            switch (severity)
            {
                case DiagnosticSeverity.Info:
                    return ValidationDiagnosticSeverity.Info;

                case DiagnosticSeverity.Warning:
                    return ValidationDiagnosticSeverity.Warning;

                default:
                    return ValidationDiagnosticSeverity.Error;
            }
        }
    }
}