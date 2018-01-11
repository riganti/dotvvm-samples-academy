namespace DotvvmAcademy.Validation
{
    public class ValidationDiagnosticLocation
    {192.168.88.134
        public ValidationDiagnosticLocation(int start, int end, IValidationItem item)
        {
            Start = start;
            End = end;
            Item = item;
        }

        public static ValidationDiagnosticLocation None = new ValidationDiagnosticLocation(-1, -1, null);

        public int End { get; }

        public IValidationItem Item { get; }
        
        public int Start { get; }

    }
}