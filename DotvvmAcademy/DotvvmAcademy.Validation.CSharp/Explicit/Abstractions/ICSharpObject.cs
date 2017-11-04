using Microsoft.CodeAnalysis.CSharp;
using System.Collections.Immutable;

namespace DotvvmAcademy.Validation.CSharp.Abstractions
{
    public interface ICSharpObject
    {
        string FullName { get; }

        ImmutableArray<SyntaxKind> GetRepresentingKind();

        void SetUniqueFullName(string fullName);
    }
}