namespace DotvvmAcademy.Validation
{
    public interface ISourceCode
    {
        string FileName { get; }

        bool IsValidated { get; }

        string GetContent();
    }
}