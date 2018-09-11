using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Xunit;

namespace DotvvmAcademy.Validation.CSharp.Experiments
{
    public class AssemblyExperiments : CSharpExperimentBase
    {
        public const string Source1 = @"
namespace TwoAssemblies
{
    public class Problem
    {
        public int One { get; set; }
    }
}";

        public const string Source2 = @"
namespace TwoAssemblies
{
    public class Problem
    {
        public int Two { get; set; }
    }
}";

        [Fact]
        public void ConflictExperiment()
        {
            var assembly1 = GetAssembly(GetCompilation(
                sample: Source1,
                name: "TestAssembly1",
                options: new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary)));
            var assembly2 = GetAssembly(GetCompilation(
                sample: Source2,
                name: "TestAssembly2",
                options: new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary)));
        }
    }
}