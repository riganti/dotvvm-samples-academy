using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.IO;

namespace DotvvmAcademy.Validation.CSharp.Experiments
{
    [TestClass]
    public class SymbolFullNameExperiments : CSharpExperimentBase
    {
        public const string SampleResourceName = "DotvvmAcademy.Validation.CSharp.Experiments.ShopSample.cs";

        public string Sample { get; set; }

        [TestInitialize]
        public void LoadSample()
        {
            var assembly = typeof(SymbolFullNameExperiments).Assembly;
            var stream = assembly.GetManifestResourceStream(SampleResourceName);
            using (var sr = new StreamReader(stream))
            {
                Sample = sr.ReadToEnd();
            }
        }

        [TestMethod]
        public void SymbolFullNameExperiment()
        {
            var compilation = GetCompilation(Sample, "Shop", new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));
            compilation = compilation.AddReferences(GetMetadataReference("System.Linq"));
            var roslyn = new NameAggregationRoslynVisitor();
            roslyn.Visit(compilation.Assembly);
            var definition = GetAssemblyDefinition(compilation);
            var cecil = new NameAggregationCecilVisitor();
            cecil.Visit(definition);
            var assembly = GetAssembly(compilation);
            var reflection = new NameAggregationReflectionVisitor();
            reflection.Visit(assembly);

            SerializeFullNames(roslyn.FullNames, "roslyn.txt");
            SerializeFullNames(cecil.FullNames, "cecil.txt");
            SerializeFullNames(reflection.FullNames, "reflection.txt");
        }

        private void SerializeFullNames(List<string> fullNames, string fileName)
        {
            var directory = Directory.GetCurrentDirectory();
            using (var writer = new StreamWriter(Path.Combine(directory, fileName)))
            {
                foreach (var fullName in fullNames)
                {
                    writer.WriteLine(fullName);
                }
            }
        }
    }
}