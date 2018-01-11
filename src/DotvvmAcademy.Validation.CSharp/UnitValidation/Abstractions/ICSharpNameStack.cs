namespace DotvvmAcademy.Validation.CSharp.UnitValidation.Abstractions
{
    public interface ICSharpNameStack
    {
        string PopName();

        void PushName(string name);
    }
}