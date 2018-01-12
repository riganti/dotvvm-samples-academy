namespace DotvvmAcademy.Validation.CSharp.Unit
{
    public static class CSharpClassExtensions
    {
        public static ICSharpMethod GetMethod(this ICSharpClass csharpClass, string name)
        {
            return csharpClass.GetMethod(name, null, null);
        }
    }
}