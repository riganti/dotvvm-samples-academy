namespace DotvvmAcademy.Validation
{
    public abstract partial class ValidationDiagnosticLocation
    {
        private class GlobalLocation : ValidationDiagnosticLocation
        {
            public GlobalLocation() : base(-1, -1)
            {
            }

            public override object GetNativeObject()
            {
                return null;
            }
        }
    }
}