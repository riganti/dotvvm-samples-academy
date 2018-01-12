namespace DotvvmAcademy.Validation.CSharp.Unit.Abstractions
{
    public interface ICSharpNameStack
    {
        string PopName();

        void PushName(string name);
    }
}