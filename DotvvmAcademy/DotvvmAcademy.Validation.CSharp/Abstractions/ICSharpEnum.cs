namespace DotvvmAcademy.Validation.CSharp.Abstractions
{
    /// <summary>
    /// A C# enum.
    /// </summary>
    public interface ICSharpEnum : ICSharpAllowsAccessModifier
    {
        void Member(string name);

        void MemberCount(int count);
    }
}