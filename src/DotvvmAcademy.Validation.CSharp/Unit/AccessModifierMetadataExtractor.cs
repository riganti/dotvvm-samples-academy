using DotvvmAcademy.Validation.CSharp.StaticAnalysis;
using DotvvmAcademy.Validation.CSharp.Unit;
using Microsoft.CodeAnalysis;
using System.Collections.Immutable;
using System.Linq;

namespace DotvvmAcademy.Validation.CSharp.Unit
{
    public class AccessModifierMetadataExtractor : IMetadataExtractor
    {
        public void ExtractMetadata(ImmutableDictionary<string, ICSharpObject> csharpObjects, CSharpStaticAnalysisContext context)
        {
            var builder = ImmutableDictionary.CreateBuilder<string, IStaticAnalysisMetadata>();
            var accessible = csharpObjects.Values.OfType<ICSharpAllowsAccessModifier>();
            foreach (var csharpObject in accessible)
            {
                builder.Add(csharpObject.FullName, new AccessModifierMetadata
                {
                    Accessibility = ToAccessibility(csharpObject.AccessModifier)
                });
            }
            context.AddMetadata<AccessModifierAnalyzer>(builder.ToImmutable());
        }

        private Accessibility ToAccessibility(CSharpAccessModifier accessModifier)
        {
            switch (accessModifier)
            {
                case CSharpAccessModifier.Public:
                    return Accessibility.Public;

                case CSharpAccessModifier.Private:
                    return Accessibility.Private;

                case CSharpAccessModifier.ProtectedInternal:
                    return Accessibility.ProtectedAndInternal;

                case CSharpAccessModifier.Protected:
                    return Accessibility.Protected;

                case CSharpAccessModifier.Internal:
                    return Accessibility.Internal;

                default:
                    return Accessibility.NotApplicable;
            }
        }
    }
}