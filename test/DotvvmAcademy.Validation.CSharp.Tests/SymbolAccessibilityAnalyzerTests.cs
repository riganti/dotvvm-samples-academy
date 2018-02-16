using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace DotvvmAcademy.Validation.CSharp.Tests
{
    [TestClass]
    public class SymbolAccessibilityAnalyzerTests : AnalyzerTestBase
    {
        public const string Sample = @"
public class SampleClass
{
    private readonly string testField;

    internal string TestProperty { get; set; }

    protected void TestMethod()
    {
    }
}";

        [TestMethod]
        public async Task BasicSymbolAccessibilityAnalyzerTests()
        {
            var @string = MetadataName.CreateTypeName("System", "String");

            var sampleClass = MetadataName.CreateTypeName("", "SampleClass");
            var testField = MetadataName.CreateFieldOrEventName(@string, sampleClass, "testField");
            var testProperty = MetadataName.CreatePropertyName(@string, sampleClass, "TestProperty");
            var testMethod = MetadataName.CreateMethodName(sampleClass, "TestMethod");

            var metadata = new MetadataCollection();
            metadata[sampleClass, SymbolAccessibilityAnalyzer.MetadataKey] = DesiredAccessibility.Public;
            metadata[testField, SymbolAccessibilityAnalyzer.MetadataKey] = DesiredAccessibility.Private | DesiredAccessibility.Protected;
            metadata[testProperty, SymbolAccessibilityAnalyzer.MetadataKey] = DesiredAccessibility.Protected | DesiredAccessibility.Public;

            var compilation = GetCompilation(Sample);
            var nameProvider = new RoslynMetadataNameProvider();
            var locator = new SymbolLocator(compilation, nameProvider);
            var analyzer = new SymbolAccessibilityAnalyzer(metadata, locator);

            var result = await TestAnalyzer(compilation, analyzer);
            Assert.AreEqual(1, result.Length);
            Assert.AreEqual(result[0].Id, "TEMP04");
        }
    }
}