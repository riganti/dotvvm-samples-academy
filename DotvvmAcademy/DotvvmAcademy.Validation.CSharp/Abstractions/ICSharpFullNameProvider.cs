namespace DotvvmAcademy.Validation.CSharp.Abstractions
{
    public interface ICSharpFullNameProvider
    {
        string GetNestedNamespaceFullName(string superNamespace, string subNamespace);
    }
}