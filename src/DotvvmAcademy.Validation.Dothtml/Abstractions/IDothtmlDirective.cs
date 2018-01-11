namespace DotvvmAcademy.Validation.Dothtml.Abstractions
{
    /// <summary>
    /// A dothtml directive e.g. @viewModel, @masterPage, etc.
    /// </summary>
    public interface IDothtmlDirective
    {
        void Value(string value);
    }
}