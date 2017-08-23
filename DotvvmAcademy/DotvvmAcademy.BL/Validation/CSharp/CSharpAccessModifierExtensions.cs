using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Text;

namespace DotvvmAcademy.BL.Validation.CSharp
{
    public static class CSharpAccessModifierExtensions
    {
        public static Accessibility ToCodeAnalysis(this CSharpAccessModifier access)
        {
            switch (access)
            {
                case CSharpAccessModifier.Private:
                    return Accessibility.Private;
                case CSharpAccessModifier.ProtectedInternal:
                    return Accessibility.ProtectedAndInternal;
                case CSharpAccessModifier.Protected:
                    return Accessibility.Protected;
                case CSharpAccessModifier.Internal:
                    return Accessibility.Internal;
                case CSharpAccessModifier.Public:
                    return Accessibility.Public;
                default:
                    throw new ArgumentException($"The {nameof(CSharpAccessModifier)} value is not valid.", nameof(access));
            }
        }

        public static string ToHumanReadable(this CSharpAccessModifier access)
        {
            switch (access)
            {
                case CSharpAccessModifier.Private:
                    return "private";
                case CSharpAccessModifier.ProtectedInternal:
                    return "protected internal";
                case CSharpAccessModifier.Protected:
                    return "protected";
                case CSharpAccessModifier.Internal:
                    return "internal";
                case CSharpAccessModifier.Public:
                    return "public";
                default:
                    throw new ArgumentException($"The {nameof(CSharpAccessModifier)} value is not valid.", nameof(access));
            }
        }
    }
}
