namespace DotvvmAcademy.Validation
{
    public interface IValidationDiagnostic
    {
        int End { get; }

        string Message { get; }

        ValidationSeverity Severity { get; }

        int Start { get; }

        object UnderlyingObject { get; }
    }
}