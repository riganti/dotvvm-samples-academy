using DotvvmAcademy.Validation.CSharp.Unit;

namespace DotvvmAcademy.Validation.Dothtml.Unit
{
    /// <summary>
    /// A collection of dothtml controls.
    /// </summary>
    public interface IDothtmlControlCollection
    {
        IDothtmlControl this[int index] { get; }

        void Count(int count);

        IDothtmlControlCollection OfControlType(CSharpTypeDescriptor type);

        IDothtmlControlCollection OfTag(string tag);

        IDothtmlControlCollection OfPrefix(string prefix);
    }
}