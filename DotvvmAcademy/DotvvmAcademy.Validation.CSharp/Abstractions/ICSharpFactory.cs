namespace DotvvmAcademy.Validation.CSharp.Abstractions
{
    public interface ICSharpFactory
    {
        ICSharpClass CreateClass();

        ICSharpConstructor CreateConstructor();

        ICSharpDelegate CreateDelegate();

        ICSharpDocument CreateDocument();

        ICSharpEnum CreateEnum();

        ICSharpEvent CreateEvent();

        ICSharpField CreateField();

        ICSharpIndexer CreateIndexer();

        ICSharpInterface CreateInterface();

        ICSharpMethod CreateMethod();

        ICSharpNamespace CreateNamespace();

        ICSharpProperty CreateProperty();

        ICSharpStruct CreateStruct();
    }
}