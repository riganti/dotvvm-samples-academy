using DotvvmAcademy.Validation.CSharp;
using DotvvmAcademy.Validation.CSharp.StaticAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Immutable;
using System.Diagnostics;

namespace DotvvmAcademy.Validation.Tests
{
    [TestClass]
    public class CSharpTests
    {
        [TestMethod]
        public void ManualContextCreationTest()
        {
            var validator = CSharpValidationUtilities.CreateValidator();
            var request = CSharpValidationUtilities.CreateRequest(CSharpSampleSources.Sample);
            request.StaticAnalysis = GetStaticContext();
            var response = validator.Validate(request).Result;
            foreach (var diagnostic in response.Diagnostics)
            {
                Debug.WriteLine($"ValidationDiagnostic {diagnostic.Id}: \"{diagnostic.Message}\".");
            }
        }

        private CSharpStaticAnalysisContext GetStaticContext()
        {
            var context = new CSharpStaticAnalysisContext();
            var builder = ImmutableDictionary.CreateBuilder<string, RequiredSymbolMetadata>();
            builder.Add("Test", new RequiredSymbolMetadata { PossibleKind = ImmutableArray.Create(SyntaxKind.ClassDeclaration) });
            builder.Add("Test.NonExistentMethod(string)", new RequiredSymbolMetadata { PossibleKind = ImmutableArray.Create(SyntaxKind.MethodDeclaration) });
            context.AddMetadata(builder.ToImmutable());
            return context;
        }
    }
}