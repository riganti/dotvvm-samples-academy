using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DotvvmAcademy.Validation.CSharp.Tests
{
    [TestClass]
    public class TypeKindAnalyzerTests : AnalyzerTestBase
    {
        public const string Sample = @"
public class SampleClass
{
}

public struct SampleStruct
{
}

public enum SampleEnum
{
One
}

public delegate void SampleDelegate();";

        [TestMethod]
        public async Task BasicTypeKingAnalyzerTest()
        {
            var sampleClass = Factory.CreateTypeName("", "SampleClass");
            var sampleStruct = Factory.CreateTypeName("", "SampleStruct");
            var sampleEnum = Factory.CreateTypeName("", "SampleEnum");
            var sampleDelegate = Factory.CreateTypeName("", "SampleDelegate");

            var metadata = new OldMetadataCollection();
            metadata[sampleClass, TypeKindAnalyzer.MetadataKey] = CSharpTypeKind.Class;
            metadata[sampleStruct, TypeKindAnalyzer.MetadataKey] = CSharpTypeKind.Class | CSharpTypeKind.Struct;
            metadata[sampleEnum, TypeKindAnalyzer.MetadataKey] = CSharpTypeKind.Array | CSharpTypeKind.Pointer;

            var compilation = GetCompilation(Sample);
            var nameProvider = new RoslynMetadataNameProvider(Factory);
            var locator = new SymbolLocator(compilation, nameProvider);
            var analyzer = new TypeKindAnalyzer(metadata, locator);

            var result = await TestAnalyzer(compilation, analyzer);
            Assert.AreEqual(1, result.Length);
            Assert.AreEqual(result[0].Id, "TEMP07");
        }
    }
}
