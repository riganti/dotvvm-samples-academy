using System.Collections.Immutable;

namespace DotvvmAcademy.Validation.CSharp.UnitValidation.Abstractions
{
    public interface ICSharpObjectFactory
    {
        ImmutableDictionary<string, ICSharpObject> GetAllObjects();

        ICSharpDocument GetDocument();

        TCSharpObject GetObject<TCSharpObject>(string fullName)
            where TCSharpObject : ICSharpObject;

        void RemoveObject<TCSharpObject>(TCSharpObject csharpObject)
            where TCSharpObject : ICSharpObject;
    }
}