using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DotvvmAcademy.Lessons;
using DotvvmAcademy.Steps.Validation;
using DotVVM.Framework.Configuration;
using DotVVM.Framework.ViewModel;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace DotvvmAcademy.Steps
{
    public class CodeStep : StepBase, ICodeEditorStep
    {
        public CodeStep(LessonBase currentLesson) : base(currentLesson)
        {
        }

        public string Code { get; set; } = "";


        [Bind(Direction.None)]
        public string StartupCode { get; set; }

        [Bind(Direction.None)]
        public string FinalCode { get; set; }


        [Bind(Direction.None)]
        public Action<CSharpCompilation, CSharpSyntaxTree, SemanticModel, Assembly> ValidationFunction { get; set; }

        [Bind(Direction.None)]
        public List<string> AllowedTypesConstructed { get; private set; } = new List<string>();

        [Bind(Direction.None)]
        public List<string> AllowedMethodsCalled { get; private set; } = new List<string>();

        [Bind(Direction.None)]
        public List<Assembly> ReferencedAssemblies { get; } = new List<Assembly>
        {
            typeof(object).GetTypeInfo().Assembly,
            typeof(DotvvmConfiguration).GetTypeInfo().Assembly, // DotVVM.Framework
            typeof(BindAttribute).GetTypeInfo().Assembly // DotVVM.Core
        };

        public string ShadowBoxDescription { get; internal set; }

        [Bind(Direction.None)]
        public List<string> OtherFiles { get; } = new List<string>();


        public void ResetCode()
        {
            Code = StartupCode;
        }

        public void ShowCorrectCode()
        {
            Code = FinalCode;
        }

        protected override IEnumerable<string> GetErrors()
        {
            try
            {
                var tree = (CSharpSyntaxTree) CSharpSyntaxTree.ParseText(Code);
                var compilation = CSharpCompilation.Create(
                    Guid.NewGuid().ToString(),
                    new[] {tree}.Concat(OtherFiles.Select(c => CSharpSyntaxTree.ParseText(c))),
                    ReferencedAssemblies.Select(a => MetadataReference.CreateFromFile(a.Location)).ToArray(),
                    new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary)
                );

                var assembly = compilation.CompileToAssembly();
                var model = compilation.GetSemanticModel(tree);

                var validationVisitor = new CSharpCodeSafetyVisitor(this, compilation, tree, model);
                validationVisitor.Visit(tree.GetCompilationUnitRoot());

                ValidationFunction(compilation, tree, model, assembly);
                return Enumerable.Empty<string>();
            }
            catch (CodeValidationException ex)
            {
                return new[] {ex.Message};
            }
        }
    }
}