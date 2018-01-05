using System;
using DotVVM.Framework.Configuration;
using DotVVM.Framework.ViewModel;
using DotvvmAcademy.Steps.StepsBases;
using DotvvmAcademy.Steps.Validation;
using DotvvmAcademy.Steps.Validation.Interfaces;
using DotvvmAcademy.Steps.Validation.Validators.CommonValidators;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace DotvvmAcademy.Steps
{
    public class CodeStepCsharp : CodeStepBase<ICSharpCodeValidationObject>
    {
        private MetadataReference[] metadataReferences = new MetadataReference[]
        {
            GetMetadataReference("netstandard"),
            GetMetadataReference<object>(),
            GetMetadataReference("System.Runtime"),
            GetMetadataReference<BindAttribute>(),
            GetMetadataReference<RequiredAttribute>(),
            GetMetadataReference<DotvvmConfiguration>()
        };

        [Bind(Direction.None)]
        public List<string> AllowedMethodsCalled { get; private set; } = new List<string>();

        [Bind(Direction.None)]
        public List<string> AllowedTypesConstructed { get; private set; } = new List<string>();

        [Bind(Direction.None)]
        public List<string> OtherCodeDependencies { get; } = new List<string>();

        [Bind(Direction.None)]
        public List<string> ReferencedAssemblies { get; }

        public override ICSharpCodeValidationObject Validator { get; set; }

        protected override IEnumerable<string> GetValidationErrors()
        {
            try
            {
                var tree = (CSharpSyntaxTree)CSharpSyntaxTree.ParseText(Code);

                var syntaxTrees = new[] { tree }.Concat(OtherCodeDependencies.Select(c => CSharpSyntaxTree.ParseText(c)));

                var compilation = CSharpCompilation.Create(
                    Guid.NewGuid().ToString(),
                    syntaxTrees,
                    metadataReferences,
                    new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary)
                );

                var assembly = compilation.CompileToAssembly();
                var model = compilation.GetSemanticModel(tree);

                var validationVisitor = new CSharpCodeSafetyVisitor(this, model);
                validationVisitor.Visit(tree.GetCompilationUnitRoot());
                Validator.Validate(compilation, tree, model, assembly);
                return Enumerable.Empty<string>();
            }
            catch (CodeValidationException ex)
            {
                return new[] { ex.Message };
            }
        }

        private static MetadataReference GetMetadataReference<TType>()
        {
            return MetadataReference.CreateFromFile(typeof(TType).Assembly.Location);
        }

        private static MetadataReference GetMetadataReference(string name)
        {
            return MetadataReference.CreateFromFile(Assembly.Load(new AssemblyName(name)).Location);
        }
    }
}