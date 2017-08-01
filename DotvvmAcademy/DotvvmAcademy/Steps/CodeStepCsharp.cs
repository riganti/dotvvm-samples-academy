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
        public CodeStepCsharp()
        {
            ReferencedAssemblies = new List<string>
            {
                GetAssemblyLocationFromType(typeof(object)),
                GetAssemblyLocationFromType(typeof(DotvvmConfiguration)),
                GetAssemblyLocationFromType(typeof(BindAttribute)),
                GetAssemblyLocationFromType(typeof(RequiredAttribute)),
                GetAssemblyLocationFromType(typeof(Attribute)),
                GetAssemblyLocationFromType(typeof(ExtensionAttribute)),
                Assembly.Load(new AssemblyName("System.Runtime")).Location,
                Assembly.Load(new AssemblyName("mscorlib")).Location
            };
        }

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

                var portableExecutableReferences =
                    ReferencedAssemblies.Select(path => MetadataReference.CreateFromFile(path)).ToArray();
                var syntaxTrees = new[] { tree }.Concat(OtherCodeDependencies.Select(c => CSharpSyntaxTree.ParseText(c)));

                var compilation = CSharpCompilation.Create(
                    Guid.NewGuid().ToString(),
                    syntaxTrees,
                    portableExecutableReferences,
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

        private static string GetAssemblyLocationFromType(Type type)
        {
            return type.GetTypeInfo().Assembly.Location;
        }
    }
}