namespace DotvvmAcademy.Validation.CSharp.Unit.Abstractions
{
    public static class CSharpNamespaceExtensions
    {
        public static ICSharpClass GetClass(this ICSharpNamespace csharpNamespace, string name)
        {
            return csharpNamespace.GetClass(name, null);
        }
    }
}