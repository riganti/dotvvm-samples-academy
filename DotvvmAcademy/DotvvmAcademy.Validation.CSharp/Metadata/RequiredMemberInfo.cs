using Microsoft.CodeAnalysis.CSharp;

namespace DotvvmAcademy.Validation.CSharp.Metadata
{
    public class RequiredMemberInfo
    {
        public RequiredMemberInfo(string fullName, SyntaxKind kind)
        {
            FullName = fullName;
            Kind = kind;
        }

        public string FullName { get; }

        public SyntaxKind Kind { get; }
    }
}