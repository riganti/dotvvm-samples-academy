using DotvvmAcademy.Validation.Unit;

namespace DotvvmAcademy.Validation.Dothtml.Unit
{
    /// <summary>
    /// A dothtml view.
    /// </summary>
    public interface IDothtmlView : IValidationUnit
    {
        IDothtmlControl GetRoot();
    }
}