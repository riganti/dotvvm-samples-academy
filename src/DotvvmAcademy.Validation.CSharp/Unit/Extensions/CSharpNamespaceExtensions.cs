namespace DotvvmAcademy.Validation.CSharp.Unit
{
    public static class CSharpNamespaceExtensions
    {
        public static ICSharpClass GetClass(this ICSharpNamespace csharpNamespace, string name)
        {
            return csharpNamespace.GetClass(name, null);
        }
    }
}