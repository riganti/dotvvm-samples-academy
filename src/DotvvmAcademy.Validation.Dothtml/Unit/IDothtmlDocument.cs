using DotvvmAcademy.Validation.Unit;

namespace DotvvmAcademy.Validation.Dothtml.Unit
{
    /// <summary>
    /// A dothtml view.
    /// </summary>
    public interface IDothtmlDocument : IValidationUnit
    {
        IDothtmlControl GetControl(string path);
    }
}