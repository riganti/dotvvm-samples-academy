using DotvvmAcademy.Validation.Unit;

namespace DotvvmAcademy.Validation.Dothtml.Unit
{
    public static class DothtmlUnitExtensions
    {
        public const string ViewModelKey = "ViewModel";

        public static void SetViewModelPath(this DothtmlUnit unit, string sourcePath)
            => unit.SetSourcePath(ViewModelKey, sourcePath);
    }
}