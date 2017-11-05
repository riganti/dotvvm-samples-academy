namespace DotvvmAcademy.Validation.CSharp.Abstractions
{
    public static class CSharpClassExtensions
    {
        public static ICSharpMethod GetMethod(this ICSharpClass csharpClass, string name)
        {
            return csharpClass.GetMethod(name, null, null);
        }
    }
}