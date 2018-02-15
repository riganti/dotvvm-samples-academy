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
            var @int = MetadataName.CreateTypeName("System", "Int32");
            var sampleClass = MetadataName.CreateTypeName("", "SampleClass");
            var sampleStruct = MetadataName.CreateTypeName("", "SampleStruct");
            var classMethod = MetadataName.CreateMethodName(sampleClass, "ClassMethod");
            var structMethod = MetadataName.CreateMethodName(sampleStruct, "StructMethod");
            var property = MetadataName.CreatePropertyName(@int, sampleStruct, "Property");

            var metadata = new MetadataCollection();
            metadata[sampleClass, SymbolStaticAnalyzer.MetadataKey] = true;
            metadata[sampleStruct, SymbolStaticAnalyzer.MetadataKey] = true;
            metadata[classMethod, SymbolStaticAnalyzer.MetadataKey] = false;

            var compilation = GetCompilation(Sample);
            var nameProvider = new RoslynMetadataNameProvider();
            var locator = new SymbolLocator(compilation, nameProvider);
            var analyzer = new SymbolStaticAnalyzer(metadata, locator);

            var result = await TestAnalyzer(compilation, analyzer);
            Assert.AreEqual(2, result.Length);
        }
    }
}
