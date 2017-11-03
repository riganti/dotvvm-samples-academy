using DotvvmAcademy.Validation.CSharp.Abstractions;
using Microsoft.CodeAnalysis.CSharp;
using System;
using System.Collections.Immutable;

namespace DotvvmAcademy.Validation.CSharp
{
    public class SyntaxKindConverter
    {
        public SyntaxKind Convert(ICSharpObject csharpObject)
        {
            switch (csharpObject)
            {
                case ICSharpClass c:
                    return SyntaxKind.ClassDeclaration;
                case ICSharpStruct s:
                    return SyntaxKind.StructConstraint;
                case ICSharpProperty p:
                    return SyntaxKind.PropertyDeclaration:
                case ICSharpAccessor a:
                    if(a.FullName)
                default:
                    return SyntaxKind.None;
            }
        }
    }
}