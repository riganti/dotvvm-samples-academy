using DotvvmAcademy.Validation.CSharp;

namespace DotvvmAcademy.Validation.Dothtml.Abstractions
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