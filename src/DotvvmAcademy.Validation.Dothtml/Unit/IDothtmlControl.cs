using DotvvmAcademy.Validation.CSharp.Unit;

namespace DotvvmAcademy.Validation.Dothtml.Unit
{
    /// <summary>
    /// A dothtml control e.g. HtmlGenericControl, TextBox, Button, etc.
    /// </summary>
    public interface IDothtmlControl
    {
        IDothtmlControlCollection Children();

        void Prefix(string prefix);

        IDothtmlProperty Property(string name);

        void Tag(string tag);

        void Type(CSharpTypeDescriptor controlType);
    }
}