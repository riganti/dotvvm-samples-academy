using Microsoft.CodeAnalysis.CSharp;
using System;

namespace DotvvmAcademy.Validation.CSharp.Unit
{
    [AttributeUsage(AttributeTargets.Interface, Inherited = true, AllowMultiple = true)]
    public sealed class SyntaxKindAttribute : Attribute
    {
        public SyntaxKindAttribute(SyntaxKind syntaxKind)
        {
            SyntaxKind = syntaxKind;
        }

        public SyntaxKind SyntaxKind { get; }
    }
}