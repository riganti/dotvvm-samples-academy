namespace DotvvmAcademy.Validation.CSharp.Unit
{
    public interface ICSharpNameStack
    {
        string PopName();

        void PushName(string name);
    }
}