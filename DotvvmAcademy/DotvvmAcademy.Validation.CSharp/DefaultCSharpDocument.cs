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

        public ICSharpNamespace GlobalNamespace()
        {
            return factory.CreateNamespace();
        }

        public ICSharpNamespace Namespace(string name)
        {
            return factory.CreateNamespace(name);
        }
    }
}