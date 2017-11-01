using DotvvmAcademy.Validation.CSharp.Abstractions;

namespace DotvvmAcademy.Validation.CSharp
{
    public class DefaultCSharpDocument : ICSharpDocument
    {
        private readonly ICSharpFactory factory;

        public DefaultCSharpDocument(ICSharpFactory factory)
        {
            this.factory = factory;
        }

        public ICSharpNamespace GetGlobalNamespace()
        {
            return factory.GetObject<ICSharpNamespace>("");
        }

        public ICSharpNamespace GetNamespace(string name)
        {
            return factory.GetObject<ICSharpNamespace>(name);
        }

        public void Remove<TCSharpObject>(TCSharpObject csharpObject) where TCSharpObject : ICSharpObject
        {
            factory.RemoveObject(csharpObject);
        }
    }
}