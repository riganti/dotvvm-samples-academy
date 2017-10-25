using System.Collections.Generic;

namespace DotvvmAcademy.Validation.Abstractions
{
    public interface IValidationRequest
    {
        List<string> ValidationMethods { get; }
    }
}