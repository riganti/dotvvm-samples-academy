namespace DotvvmAcademy.Validation.CSharp.Abstractions
{
    public interface ICSharpFactory
    {
        TCSharpObject GetObject<TCSharpObject>(string fullName)
            where TCSharpObject : ICSharpObject;

        void RemoveObject<TCSharpObject>(TCSharpObject csharpObject)
            where TCSharpObject : ICSharpObject;

        CSharpValidationMethod CreateValidationMethod();
    }
}