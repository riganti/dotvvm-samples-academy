using Microsoft.CodeAnalysis;
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
            var @string = Factory.CreateTypeName("System", "String");

            var sampleClass = Factory.CreateTypeName("", "SampleClass");
            var testField = Factory.CreateFieldName(sampleClass, "testField", @string);
            var testProperty = Factory.CreatePropertyName(sampleClass, "TestProperty", @string);
            var testMethod = Factory.CreateMethodName(sampleClass, "TestMethod");

            var metadata = new MetadataCollection<MetadataName>();
            metadata[sampleClass, SymbolAccessibilityAnalyzer.MetadataKey] = Accessibility.Public;
            metadata[testField, SymbolAccessibilityAnalyzer.MetadataKey] = Accessibility.Private | Accessibility.Protected;
            metadata[testProperty, SymbolAccessibilityAnalyzer.MetadataKey] = Accessibility.Protected | Accessibility.Public;

            var compilation = GetCompilation(Sample);
            var nameProvider = new RoslynMetadataNameProvider(Factory);
            var locator = new SymbolLocator(compilation, nameProvider);
            var analyzer = new SymbolAccessibilityAnalyzer(metadata, locator);

            var result = await TestAnalyzer(compilation, analyzer);
            Assert.AreEqual(1, result.Length);
            Assert.AreEqual(result[0].Id, "TEMP04");
        }
    }
}