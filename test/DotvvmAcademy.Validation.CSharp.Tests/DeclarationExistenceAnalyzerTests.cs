using DotvvmAcademy.Validation.CSharp;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Threading.Tasks;

namespace DotvvmAcademy.Validation.CSharp.Tests
{
    [TestClass]
    public class DeclarationExistenceAnalyzerTests : AnalyzerTestBase
    {
        public const string BasicSample = @"
namespace SampleNamespace
{
    public class SampleClass
    {
        public int IntegerProperty { get; set; }

        public string GetString()
        {
            return IntegerProperty.ToString() + ObjectProperty.ToString();
        }
    }
}";

        private MetadataName sampleClass;
        private MetadataName integerProperty;
        private MetadataName getString;

        public DeclarationExistenceAnalyzerTests()
        {
            var int32 = MetadataName.CreateTypeName("System", "Int32");
            var @string = MetadataName.CreateTypeName("System", "String");

            sampleClass = MetadataName.CreateTypeName("SampleNamespace", "SampleClass");
            integerProperty = MetadataName.CreatePropertyName(int32, sampleClass, "IntegerProperty");
            getString = MetadataName.CreateMethodName(sampleClass, "GetString", @string);
        }

        [TestMethod]
        public async Task BasicDeclarationExistenceAnalyzerTest()
        {
            var metadata = new MetadataCollection();
            metadata[sampleClass, DeclarationExistenceAnalyzer.MetadataKey] = true;
            metadata[integerProperty, DeclarationExistenceAnalyzer.MetadataKey] = true;
            metadata[getString, DeclarationExistenceAnalyzer.MetadataKey] = false;

            var compilation = GetCompilation(BasicSample);
            var nameProvider = new RoslynMetadataNameProvider();
            var locator = new SymbolLocator(compilation, nameProvider);
            var analyzer = new DeclarationExistenceAnalyzer(metadata, locator);
            var results = await TestAnalyzer(compilation, analyzer);

            Assert.AreEqual(1, results.Length);
            Assert.AreEqual(results.Single().Id, "TEMP01");
        }
    }
}