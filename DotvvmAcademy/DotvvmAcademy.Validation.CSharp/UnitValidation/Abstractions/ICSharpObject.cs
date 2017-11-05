using Microsoft.CodeAnalysis.CSharp;
using System.Collections.Immutable;

namespace DotvvmAcademy.Validation.CSharp.UnitValidation.Abstractions
{
    public interface ICSharpObject
    {
        string FullName { get; }

        ImmutableArray<SyntaxKind> Kinds { get; }

        void SetUniqueFullName(string fullName);
    }
}