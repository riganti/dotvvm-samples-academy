namespace DotvvmAcademy.Validation.CSharp.Unit.Abstractions
{
    /// <summary>
    /// A C# member or type that be marked as constant.
    /// </summary>
    public interface ICSharpAllowsConstModifier : ICSharpObject
    {
        bool IsConstant { get; set; }
    }
}