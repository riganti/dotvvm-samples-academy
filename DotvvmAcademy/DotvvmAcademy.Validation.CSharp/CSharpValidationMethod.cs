using System.Collections.Immutable;
using System.Linq;

namespace DotvvmAcademy.Validation.CSharp
{
    public class CSharpValidationMethod
    {
        public ImmutableArray<string> RequiredSymbols { get; set; }

        public CSharpValidationMethod Merge(CSharpValidationMethod other)
        {
            var method = new CSharpValidationMethod
            {
                RequiredSymbols = RequiredSymbols.Concat(other.RequiredSymbols).ToImmutableArray()
            };
            return method;
        }
    }
}