using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Immutable;
using System.Threading.Tasks;

namespace DotvvmAcademy.Validation.CSharp.Tests
{
    [TestClass]
    public class SymbolAllowedAnalyzerTests : AnalyzerTestBase
    {
        public const string Sample = @"
public class TestClass
{
    public void TestMethod(string s)
    {
        s.Clone();
        s.CompareTo("""");
        s.Contains(""Test"");
        s.EndsWith(""Test"");
    }
}";

        [TestMethod]
        public async Task BasicSymbolAllowedAnalyzerTest()
        {
            var @void = Factory.CreateTypeName("System", "Void");
            var @object = Factory.CreateTypeName("System", "Object");
            var @int = Factory.CreateTypeName("System", "Int32");
            var @string = Factory.CreateTypeName("System", "String");
            var @bool = Factory.CreateTypeName("System", "Bool");

            var clone = Factory.CreateMethodName(@string, "Clone", @object);
            var compareTo = Factory.CreateMethodName(@string, "CompareTo", @int, parameters: ImmutableArray.Create(@string));
            var contains = Factory.CreateMethodName(@string, "Contains", @bool, parameters: ImmutableArray.Create(@string));
            var endsWith = Factory.CreateMethodName(@string, "EndsWith", @bool, parameters: ImmutableArray.Create(@string));

            var metadata = new MetadataCollection<MetadataName>();
            metadata[clone, SymbolAllowedAnalyzer.MetadataKey] = true;
            metadata[compareTo, SymbolAllowedAnalyzer.MetadataKey] = true;
            metadata[@string, SymbolAllowedAnalyzer.MetadataKey] = true;
            metadata[@void, SymbolAllowedAnalyzer.MetadataKey] = true;
            metadata[contains, SymbolAllowedAnalyzer.MetadataKey] = false;

            var nameProvider = new RoslynMetadataNameProvider(Factory);
            var analyzer = new SymbolAllowedAnalyzer(metadata, nameProvider);
            var compilation = GetCompilation(Sample);
            var result = await TestAnalyzer(compilation, analyzer);

            Assert.AreEqual(2, result.Length);
        }
    }
}