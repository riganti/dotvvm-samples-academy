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
            return factory.CreateNamespace("");
        }

        public ICSharpNamespace GetNamespace(string name)
        {
            return factory.CreateNamespace(name);
        }

        public void Remove<TCSharpObject>(TCSharpObject csharpObject) where TCSharpObject : ICSharpObject
        {
            throw new System.NotImplementedException();
        }
    }
}