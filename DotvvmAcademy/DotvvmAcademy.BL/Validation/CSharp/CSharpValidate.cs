using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System;
using System.IO;
using System.Reflection;
using System.Runtime.Loader;

namespace DotvvmAcademy.BL.Validation.CSharp
{
    public sealed class CSharpValidate : Validate
    {
        public CSharpValidate(string code) : base(code)
        {
        }

        public CSharpRoot Root { get; private set; }

        public string UserCodeAssemblyName => $"{nameof(DotvvmAcademy)}.UserCode.{Guid.NewGuid()}";

        internal Assembly Assembly { get; private set; }

        internal CSharpCompilation Compilation { get; private set; }

        internal SemanticModel Model { get; private set; }

        internal SyntaxTree Tree { get; private set; }

        protected override void Init()
        {
            Tree = CSharpSyntaxTree.ParseText(Code);
            Compilation = CSharpCompilation.Create(UserCodeAssemblyName, new[] { Tree });
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
                    AddError("The code couldn't be compiled.", 0, 0);
                    return null;
                }
                stream.Position = 0;

                return AssemblyLoadContext.Default.LoadFromStream(stream);
            }
        }
    }
}