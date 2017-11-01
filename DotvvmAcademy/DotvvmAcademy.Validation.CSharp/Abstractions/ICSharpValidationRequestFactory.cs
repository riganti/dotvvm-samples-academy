using System;
using System.Collections.Generic;
using System.Text;

namespace DotvvmAcademy.Validation.CSharp.Abstractions
{
    public interface ICSharpValidationRequestFactory : ICSharpObject
    {
        CSharpValidationRequest CreateRequest(string source, string validationMethodName);
    }
}
