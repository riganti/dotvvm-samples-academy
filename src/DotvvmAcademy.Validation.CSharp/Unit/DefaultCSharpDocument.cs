using DotvvmAcademy.Validation.CSharp.Unit;

namespace DotvvmAcademy.Validation.CSharp.Unit
{
    public class DefaultCSharpDocument : ICSharpDocument
    {
        private readonly ICSharpObjectFactory factory;

        public DefaultCSharpDocument(ICSharpObjectFactory factory)
        {
            this.factory = factory;
        }

        public ICSharpNamespace GetGlobalNamespace()
        {
            return factory.GetObject<ICSharpNamespace>(CSharpConstants.GlobalNamespaceName);
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