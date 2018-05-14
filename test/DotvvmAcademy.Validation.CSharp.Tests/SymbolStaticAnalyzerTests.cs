using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DotvvmAcademy.Validation.CSharp.Tests
{
    [TestClass]
    public class SymbolStaticAnalyzerTests : AnalyzerTestBase
    {
        public const string Sample = @"
public static class SampleClass
{
    public static void ClassMethod()
    {
    }
}

public struct SampleStruct
{
    public void StructMethod()
    {
    }

    public static int Property { get; set; }
}";

        [TestMethod]
        public async Task BasicSymbolStaticAnalyzerTest()
        {
            var @int = Factory.CreateTypeName("System", "Int32");
            var sampleClass = Factory.CreateTypeName("", "SampleClass");
            var sampleStruct = Factory.CreateTypeName("", "SampleStruct");
            var classMethod = Factory.CreateMethodName(sampleClass, "ClassMethod");
            var structMethod = Factory.CreateMethodName(sampleStruct, "StructMethod");
            var property = Factory.CreatePropertyName(sampleStruct, "Property", @int);

            var metadata = new MetadataCollection<MetadataName>();
            metadata[sampleClass, SymbolStaticAnalyzer.MetadataKey] = true;
            metadata[sampleStruct, SymbolStaticAnalyzer.MetadataKey] = true;
            metadata[classMethod, SymbolStaticAnalyzer.MetadataKey] = false;

            var compilation = GetCompilation(Sample);
            var nameProvider = new RoslynMetadataNameProvider(Factory);
            var locator = new SymbolLocator(compilation, nameProvider);
            var analyzer = new SymbolStaticAnalyzer(metadata, locator);

            var result = await TestAnalyzer(compilation, analyzer);
            Assert.AreEqual(2, result.Length);
        }
    }
}
