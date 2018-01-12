using System.Reflection;
using System.Collections.Generic;
using System.Linq;

namespace DotvvmAcademy.Validation.CSharp
{
    public class CSharpValidationResponse : IValidationResponse
    {
        public CSharpValidationResponse(Assembly emittedAssembly, IEnumerable<ValidationDiagnostic> diagnostics)
        {
            EmittedAssembly = emittedAssembly;
            Diagnostics = diagnostics;
        }

        public Assembly EmittedAssembly { get; }

        public IEnumerable<ValidationDiagnostic> Diagnostics { get; }
    }
}