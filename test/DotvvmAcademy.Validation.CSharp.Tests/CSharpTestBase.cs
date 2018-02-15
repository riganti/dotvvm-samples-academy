using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Mono.Cecil;
using System.IO;
using System.Reflection;
using System.Runtime.Loader;

namespace DotvvmAcademy.Validation.CSharp.Tests
{
    public class CSharpTestBase
    {
        public readonly MetadataReference[] defaultReferences;

        public CSharpTestBase()
        {
            defaultReferences = new[] {
                GetMetadataReference("System.Private.CoreLib"),
                GetMetadataReference("System.Runtime")
            };
        }

        public Assembly GetAssembly(CSharpCompilation compilation)
        {
            using (var stream = new MemoryStream())
            {
                Compile(compilation, stream);
                return AssemblyLoadContext.Default.LoadFromStream(stream);
            }
        }

        public AssemblyDefinition GetAssemblyDefinition(CSharpCompilation compilation)
        {
            using(var stream = new MemoryStream())
            {
                Compile(compilation, stream);
                return AssemblyDefinition.ReadAssembly(stream);
            }
        }

        public CSharpCompilation GetCompilation(string sample, string name = "Tests", CSharpCompilationOptions options = null)
        {
            var tree = CSharpSyntaxTree.ParseText(sample);
            return CSharpCompilation.Create(name,
                new[] { tree },
                defaultReferences,
                options);
        }

        public MetadataReference GetMetadataReference<TType>()
        {
            return MetadataReference.CreateFromFile(typeof(TType).Assembly.Location);
        }

        public MetadataReference GetMetadataReference(string assemblyName)
        {
            var assembly = AssemblyLoadContext.Default.LoadFromAssemblyName(new AssemblyName(assemblyName));
            return MetadataReference.CreateFromFile(assembly.Location);
        }

        private void Compile(CSharpCompilation compilation, Stream stream)
        {
            var result = compilation.Emit(stream);
            if (result.Success)
            {
                stream.Position = 0;
                return;
            }
            stream.Close();
            throw new CompilationUnsuccessfulException("The passed compilation could not be compiled.", result);
        }
    }
}