using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DotvvmAcademy.Validation.CSharp.Tests
{
    [TestClass]
    public class BaseTypeAnalyzerTests : AnalyzerTestBase
    {
        public const string Sample = @"
public class One
{
}

public class Two : One
{
}

public class Three : Two
{
}";

        [TestMethod]
        public async Task BasicBaseTypeAnalyzerTest()
        {
            var @object = Factory.CreateTypeName("System", "Object");
            var one = Factory.CreateTypeName("", "One");
            var two = Factory.CreateTypeName("", "Two");
            var three  = Factory.CreateTypeName("", "Three");

            var metadata = new MetadataCollection<MetadataName>();
            metadata[one, BaseTypeAnalyzer.MetadataKey] = @object;
            metadata[two, BaseTypeAnalyzer.MetadataKey] = one;
            metadata[three, BaseTypeAnalyzer.MetadataKey] = one;

            var compilation = GetCompilation(Sample);
            var nameProvider = new RoslynMetadataNameProvider(Factory);
            var locator = new SymbolLocator(compilation, nameProvider);
            var analyzer = new BaseTypeAnalyzer(metadata, locator);

            var result = await TestAnalyzer(compilation, analyzer);
            Assert.AreEqual(1, result.Length);
            Assert.AreEqual(result[0].Id, "TEMP08");
        }
    }
}
