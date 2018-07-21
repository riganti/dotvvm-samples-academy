namespace DotvvmAcademy.Validation.CSharp
{
    public class CSharpValidationOptions : IValidationOptions
    {
        public static CSharpValidationOptions Default = new CSharpValidationOptions();

        public CSharpValidationOptions(bool includeCompilerDiagnostics = true)
        {
            IncludeCompilerDiagnostics = includeCompilerDiagnostics;
        }

        public bool IncludeCompilerDiagnostics { get; }
    }
}