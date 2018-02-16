using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Immutable;
using System.Threading.Tasks;

namespace DotvvmAcademy.Validation.CSharp.Tests
{
    [TestClass]
    public class InterfaceImplementationAnalyzerTests : AnalyzerTestBase
    {
        public const string Sample = @"
public interface FirstInterface
{
}

public interface SecondInterface
{
}

public class FirstClass : FirstInterface
{
}

public class SecondClass : FirstInterface
{
}

public class ThirdClass : FirstInterface, SecondInterface
{
}
";

        [TestMethod]
        public async Task BasicInterfaceImplementationAnalyzerTest()
        {
            var firstInterface = MetadataName.CreateTypeName("", "FirstInterface");
            var secondInterface = MetadataName.CreateTypeName("", "SecondInterface");
            var firstClass = MetadataName.CreateTypeName("", "FirstClass");
            var secondClass = MetadataName.CreateTypeName("", "SecondClass");
            var thirdClass = MetadataName.CreateTypeName("", "ThirdClass");

            var metadata = new MetadataCollection();
            metadata[firstClass, InterfaceImplementationAnalyzer.PositiveMetadataKey] = ImmutableArray.Create(firstInterface);
            metadata[secondClass, InterfaceImplementationAnalyzer.PositiveMetadataKey] = ImmutableArray.Create(secondInterface);
            metadata[secondClass, InterfaceImplementationAnalyzer.NegativeMetadataKey] = ImmutableArray.Create(firstInterface);
            metadata[thirdClass, InterfaceImplementationAnalyzer.PositiveMetadataKey] = ImmutableArray.Create(firstInterface, secondInterface);

            var compilation = GetCompilation(Sample);
            var nameProvider = new RoslynMetadataNameProvider();
            var locator = new SymbolLocator(compilation, nameProvider);
            var analyzer = new InterfaceImplementationAnalyzer(metadata, locator);

            var result = await TestAnalyzer(compilation, analyzer);
            Assert.AreEqual(2, result.Length);
        }
    }
}