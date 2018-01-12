using DotvvmAcademy.Validation.CSharp.UnitValidation.Abstractions;

namespace DotvvmAcademy.Validation.CSharp.UnitValidation
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