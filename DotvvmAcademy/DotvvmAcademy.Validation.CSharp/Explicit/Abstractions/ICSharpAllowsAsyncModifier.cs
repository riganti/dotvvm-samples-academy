namespace DotvvmAcademy.Validation.CSharp.Abstractions
{
    /// <summary>
    /// A C# member or type that can be marked as async.
    /// </summary>
    public interface ICSharpAllowsAsyncModifier : ICSharpObject
    {
        bool IsAsync { get; set; }
    }
}