namespace DotvvmAcademy.Validation.CSharp.Abstractions
{
    public interface ICSharpObject
    {
        string FullName { get; }

        void SetUniqueFullName(string fullName);
    }
}