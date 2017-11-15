using DotvvmAcademy.Validation.CSharp;
using DotvvmAcademy.Validation.CSharp.StaticAnalysis;
using Microsoft.CodeAnalysis;
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
            WriteDebugResponse(response);
        }

        [TestMethod]
        public void UnitValidationTest()
        {
            var validator = CSharpValidationUtilities.CreateValidator();
            var request = CSharpValidationUtilities.CreateRequest(CSharpSampleSources.Sample);
            var runner = CSharpValidationUtilities.CreateRunner();
            var method = typeof(CSharpSampleValidationClass).GetMethod(nameof(CSharpSampleValidationClass.SampleValidationMethod));
            (var staticContext, var _) = runner.Run(method);
            request.StaticAnalysis = staticContext;
            var response = validator.Validate(request).Result;
            WriteDebugResponse(response);
        }

        private void WriteDebugResponse(CSharpValidationResponse response)
        {
            foreach (var diagnostic in response.Diagnostics)
            {
                Debug.WriteLine($"ValidationDiagnostic {diagnostic.Id}: \"{diagnostic.Message}\".");
            }
        }

        private CSharpStaticAnalysisContext GetStaticContext()
        {
            var context = new CSharpStaticAnalysisContext();
            var allowed = ImmutableDictionary.CreateBuilder<string, AllowedSymbolMetadata>();
            allowed.Add("System.String", new AllowedSymbolMetadata());
            var accessModifiers = ImmutableDictionary.CreateBuilder<string, AccessModifierMetadata>();
            accessModifiers.Add("Test", new AccessModifierMetadata { Accessibility = Accessibility.Public });
            accessModifiers.Add("Test.TestMethod()", new AccessModifierMetadata { Accessibility = Accessibility.Internal });
            var required = ImmutableDictionary.CreateBuilder<string, RequiredSymbolMetadata>();
            required.Add("Test", new RequiredSymbolMetadata { PossibleKind = ImmutableArray.Create(SyntaxKind.ClassDeclaration) });
            required.Add("Test.NonExistentMethod(string)", new RequiredSymbolMetadata { PossibleKind = ImmutableArray.Create(SyntaxKind.MethodDeclaration) });
            context.AddMetadata(required.ToImmutable());
            context.AddMetadata(accessModifiers.ToImmutable());
            context.AddMetadata(allowed.ToImmutable());
            return context;
        }
    }
}