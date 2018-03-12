namespace DotvvmAcademy.Validation
{
    public abstract partial class ValidationDiagnosticLocation
    {
        public static ValidationDiagnosticLocation Global = new GlobalLocation();

        protected ValidationDiagnosticLocation(int start, int length)
        {
            Start = start;
            Length = length;
        }

        public int End => Start + Length;

        public bool IsEmpty => Length == 0;

        public int Length { get; }

        public int Start { get; }

        public abstract object GetNativeObject();
    }
}