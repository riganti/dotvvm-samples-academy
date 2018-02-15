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
            var @void = MetadataName.CreateTypeName("System", "Void");
            var @object = MetadataName.CreateTypeName("System", "Object");
            var @int = MetadataName.CreateTypeName("System", "Int32");
            var @string = MetadataName.CreateTypeName("System", "String");
            var @bool = MetadataName.CreateTypeName("System", "Bool");

            var clone = MetadataName.CreateMethodName(@string, "Clone", @object);
            var compareTo = MetadataName.CreateMethodName(@string, "CompareTo", @int, ImmutableArray.Create(@string));
            var contains = MetadataName.CreateMethodName(@string, "Contains", @bool, ImmutableArray.Create(@string));
            var endsWith = MetadataName.CreateMethodName(@string, "EndsWith", @bool, ImmutableArray.Create(@string));

            var metadata = new MetadataCollection();
            metadata[clone, SymbolAllowedAnalyzer.MetadataKey] = true;
            metadata[compareTo, SymbolAllowedAnalyzer.MetadataKey] = true;
            metadata[@string, SymbolAllowedAnalyzer.MetadataKey] = true;
            metadata[@void, SymbolAllowedAnalyzer.MetadataKey] = true;
            metadata[contains, SymbolAllowedAnalyzer.MetadataKey] = false;

            var nameProvider = new RoslynMetadataNameProvider();
            var analyzer = new SymbolAllowedAnalyzer(metadata, nameProvider);
            var compilation = GetCompilation(Sample);
            var result = await TestAnalyzer(compilation, analyzer);

            Assert.AreEqual(2, result.Length);
        }
    }
}