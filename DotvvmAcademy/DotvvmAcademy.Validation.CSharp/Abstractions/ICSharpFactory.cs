namespace DotvvmAcademy.Validation.CSharp.Abstractions
{
    public interface ICSharpFactory
    {
        ICSharpClass CreateClass(string fullName);

        ICSharpConstructor CreateConstructor(string fullName);

        ICSharpDelegate CreateDelegate(string fullName);

        ICSharpDocument CreateDocument();

        ICSharpEnum CreateEnum(string fullName);

        ICSharpEvent CreateEvent(string fullName);

        ICSharpField CreateField(string fullName);

        ICSharpIndexer CreateIndexer(string fullName);

        ICSharpInterface CreateInterface(string fullName);

        ICSharpMethod CreateMethod(string fullName);

        ICSharpNamespace CreateNamespace(string fullName);

        ICSharpProperty CreateProperty(string fullName);

        ICSharpStruct CreateStruct(string fullName);

        CSharpValidationMethod CreateValidationMethod();
    }
}