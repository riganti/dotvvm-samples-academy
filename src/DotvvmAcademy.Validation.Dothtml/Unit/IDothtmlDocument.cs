using DotvvmAcademy.Validation.Unit;

namespace DotvvmAcademy.Validation.Dothtml.Unit
{
    /// <summary>
    /// A dothtml view.
    /// </summary>
    public interface IDothtmlView : IValidationUnit
    {
        IDothtmlDirective Directive(string name);

        IDothtmlControl RootControl();
    }
}