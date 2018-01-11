namespace DotvvmAcademy.Validation.Dothtml.Abstractions
{
    /// <summary>
    /// A dothtml document.
    /// </summary>
    public interface IDothtmlDocument
    {
        IDothtmlDirective Directive(string name);

        IDothtmlControl RootControl();
    }
}