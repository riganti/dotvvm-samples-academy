using System.Reflection;

namespace DotvvmAcademy.Validation
{
    public interface IValidationScriptProvider
    {
        Assembly Assembly { get; }

        string Source { get; }
    }
}