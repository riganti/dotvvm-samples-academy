using DotvvmAcademy.Validation.CSharp.Metadata;
using System.Collections.Immutable;
using System.Linq;

namespace DotvvmAcademy.Validation.CSharp
{
    public class CSharpValidationMethod
    {
        public ImmutableDictionary<string, RequiredMemberInfo> RequiredMembers { get; set; }

        public CSharpValidationMethod Merge(CSharpValidationMethod other)
        {
            var method = new CSharpValidationMethod
            {
                RequiredMembers = RequiredMembers.Concat(other.RequiredMembers).ToImmutableDictionary()
            };
            return method;
        }
    }
}