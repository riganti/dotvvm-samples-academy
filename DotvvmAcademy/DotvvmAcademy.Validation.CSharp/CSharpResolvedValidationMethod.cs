using DotvvmAcademy.Validation.CSharp.Metadata;
using System.Collections.Immutable;
using System.Linq;

namespace DotvvmAcademy.Validation.CSharp
{
    public class CSharpResolvedValidationMethod
    {
        public ImmutableDictionary<string, RequiredMemberInfo> RequiredMembers { get; set; }

        public CSharpResolvedValidationMethod Merge(CSharpResolvedValidationMethod other)
        {
            var method = new CSharpResolvedValidationMethod
            {
                RequiredMembers = RequiredMembers.Concat(other.RequiredMembers).ToImmutableDictionary()
            };
            return method;
        }
    }
}