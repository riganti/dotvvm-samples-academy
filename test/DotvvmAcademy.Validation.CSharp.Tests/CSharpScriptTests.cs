using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.CodeAnalysis.Scripting.Hosting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace DotvvmAcademy.Validation.CSharp.Tests
{
    [TestClass]
    public class CSharpScriptTests : CSharpTestBase
    {
        [TestMethod]
        public void InteractiveAssemblyLoaderTest()
        {
            const string inMemoryAssemblySource = @"
public class SuperImportantType
{
    public void Do()
    {
    }
}";

            const string scriptSource = @"
#r ""SuperImportantAssembly.dll""
var sit = new SuperImportantType();
sit.Do();";

            var inMemoryAssemblyCompilation = GetCompilation(inMemoryAssemblySource, "SuperImportantAssembly", new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));
            var inMemoryAssembly = GetAssembly(inMemoryAssemblyCompilation);

            var options = ScriptOptions.Default;
            using (var assemblyLoader = new InteractiveAssemblyLoader())
            {
                assemblyLoader.RegisterDependency(inMemoryAssembly);
                var script = CSharpScript.Create(scriptSource, options, null, assemblyLoader);
                var result = script.RunAsync().Result;
            }
        }

        [TestMethod]
        public async Task RoslynScriptingHackTest()
        {
            const string hackAssemblySource = @"
public class HackClass
{
    public int Hack()
    {
        while(true)
        {
            //HAAAAAAX
        }

        return 42;
    }
}";

            const string scriptSource = @"
var hackClass = new HackClass();
return hackClass.Hack();";
            var hackAssemblyTree = CSharpSyntaxTree.ParseText(hackAssemblySource);
            var hackAssemblyCompilation = CSharpCompilation.Create("Hack", new[] { hackAssemblyTree }, DefaultReferences, new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));
            var hackAssembly = GetAssembly(hackAssemblyCompilation);
            using (var stream = new FileStream(@"<add path>", FileMode.Create))
            {
                hackAssemblyCompilation.Emit(stream);
            }
            var hackAssemblyReference = MetadataReference.CreateFromFile(@"<add path>");
            var script = CSharpScript.Create(scriptSource, ScriptOptions.Default.AddReferences(hackAssemblyReference));
            var cts = new CancellationTokenSource(10000);
            var result = await script.RunAsync(cancellationToken: cts.Token);
        }

        [TestMethod]
        public async Task RoslynScriptingTest()
        {
            const string sample = @"
while(true)
{
}

return 0;
";
            var script = CSharpScript.Create(sample);
            var cts = new CancellationTokenSource(1000);
            var result = await script.RunAsync(
                cancellationToken: cts.Token);
        }
    }
}