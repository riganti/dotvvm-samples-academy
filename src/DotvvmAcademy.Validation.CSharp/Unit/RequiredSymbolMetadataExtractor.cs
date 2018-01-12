using DotvvmAcademy.Validation.CSharp.StaticAnalysis;
using DotvvmAcademy.Validation.CSharp.Unit.Abstractions;
using Microsoft.CodeAnalysis.CSharp;
using System;
using System.Collections.Immutable;
using System.Linq;

namespace DotvvmAcademy.Validation.CSharp.Unit
{
    public class RequiredSymbolMetadataExtractor : IMetadataExtractor
    {
        private static ImmutableDictionary<Type, ImmutableArray<SyntaxKind>> syntaxKinds = GetSyntaxKinds();

        public void ExtractMetadata(ImmutableDictionary<string, ICSharpObject> csharpObjects, CSharpStaticAnalysisContext context)
        {
            var builder = ImmutableDictionary.CreateBuilder<string, IStaticAnalysisMetadata>();
            foreach (var pair in csharpObjects)
            {
                var kinds = pair.Value.GetType().GetInterfaces()
                    .SelectMany(i => syntaxKinds[i]).ToImmutableArray();
                var metadata = new RequiredSymbolMetadata
                {
                    PossibleKind = kinds
                };
                builder.Add(pair.Key, metadata);
            }
            context.AddMetadata<RequiredSymbolAnalyzer>(builder.ToImmutable());
        }

        private static ImmutableDictionary<Type, ImmutableArray<SyntaxKind>> GetSyntaxKinds()
        {
            var csharpObjectType = typeof(ICSharpObject);
            return csharpObjectType.Assembly.GetTypes()
                .Where(t => t.IsInterface && csharpObjectType.IsAssignableFrom(t))
                .ToImmutableDictionary(t => t, t =>
                {
                    return t.GetCustomAttributes(typeof(SyntaxKindAttribute), true)
                    .Cast<SyntaxKindAttribute>()
                    .Select(a => a.SyntaxKind)
                    .ToImmutableArray();
                });
        }
    }
}