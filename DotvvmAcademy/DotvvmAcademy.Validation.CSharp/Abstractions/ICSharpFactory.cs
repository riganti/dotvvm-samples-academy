using DotvvmAcademy.Validation.CSharp.Analyzers;

namespace DotvvmAcademy.Validation.CSharp.Abstractions
{
    public interface ICSharpFactory
    {
        ValidationAnalyzerContext GetValidationAnalyzerContext();

        ICSharpDocument GetDocument();

        TCSharpObject GetObject<TCSharpObject>(string fullName)
            where TCSharpObject : ICSharpObject;

        void RemoveObject<TCSharpObject>(TCSharpObject csharpObject)
            where TCSharpObject : ICSharpObject;
    }
}