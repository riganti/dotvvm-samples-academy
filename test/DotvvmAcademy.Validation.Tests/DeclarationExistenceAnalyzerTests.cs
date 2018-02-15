using DotvvmAcademy.Validation.CSharp;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace DotvvmAcademy.Validation.Tests
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

            var analyzer = new DeclarationExistenceAnalyzer(metadata);
            var results = await TestAnalyzer(analyzer, BasicSample);
        }
    }
}