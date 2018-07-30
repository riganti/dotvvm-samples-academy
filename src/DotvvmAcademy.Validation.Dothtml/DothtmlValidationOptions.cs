namespace DotvvmAcademy.Validation.Dothtml
{
    public class DothtmlValidationOptions : IValidationOptions
    {
        public static DothtmlValidationOptions Default = new DothtmlValidationOptions();

        public DothtmlValidationOptions(
            bool includeCompilerDiagnostics = true,
            string viewModel = "",
            string fileName = "View.dothtml")
        {
            IncludeCompilerDiagnostics = includeCompilerDiagnostics;
            ViewModel = viewModel;
            FileName = fileName;
        }

        public string FileName { get; }

        public bool IncludeCompilerDiagnostics { get; }

        public string ViewModel { get; }
    }
}