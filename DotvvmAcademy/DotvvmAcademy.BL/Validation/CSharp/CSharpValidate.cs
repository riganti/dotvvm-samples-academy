using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;

namespace DotvvmAcademy.BL.Validation.CSharp
{
    public sealed class CSharpValidate : Validate
    {
        public CSharpValidate(string code, IEnumerable<string> dependencies) : base(code, dependencies)
        {
        }

        public CSharpRoot Root { get; private set; }

        public string UserCodeAssemblyName => $"{nameof(DotvvmAcademy)}.UserCode.{Id}";

        internal Assembly Assembly { get; private set; }

        internal CSharpCompilation Compilation { get; private set; }

        internal SemanticModel Model { get; private set; }

        internal SyntaxTree Tree { get; private set; }

        protected override void Init()
        {
            Tree = CSharpSyntaxTree.ParseText(Code);
            var trees = Dependencies.Select(d => CSharpSyntaxTree.ParseText(d)).ToList();
            trees.Add(Tree);
            Compilation = CSharpCompilation.Create(UserCodeAssemblyName, trees, GetMetadataReferences(), GetCompilationOptions());
            Model = Compilation.GetSemanticModel(Tree);
            Assembly = EmitToAssembly(Compilation);
            Root = new CSharpRoot(this, Tree.GetCompilationUnitRoot());
        }

        private Assembly EmitToAssembly(CSharpCompilation compilation)
        {
            using (var stream = new MemoryStream())
            {
                var result = compilation.Emit(stream);
                if (!result.Success)
                {
                    AddGlobalError("The code couldn't be compiled.");
                    foreach (var diagnostic in result.Diagnostics)
                    {
                        if (diagnostic.Location.Kind == LocationKind.None)
                        {
                            AddGlobalError(diagnostic.ToString());
                        }
                        else
                        {
                            AddError(diagnostic.ToString(), diagnostic.Location.SourceSpan.Start, diagnostic.Location.SourceSpan.End);
                        }
                    }
                    return null;
                }
                stream.Position = 0;

                return AssemblyLoadContext.Default.LoadFromStream(stream);
            }
        }

        private IEnumerable<MetadataReference> GetMetadataReferences()
        {
            var types = new Type[]
            {
                typeof(object)
            };

            foreach (var type in types)
            {
                yield return MetadataReference.CreateFromFile(type.Assembly.Location);
            }
        }

        private CSharpCompilationOptions GetCompilationOptions()
        {
            var options = new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary);
            return options;
        }
    }
}