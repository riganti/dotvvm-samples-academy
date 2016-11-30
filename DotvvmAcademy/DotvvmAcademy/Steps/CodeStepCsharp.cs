using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using DotvvmAcademy.Steps.StepsBases;
using DotvvmAcademy.Steps.Validation;
using DotvvmAcademy.Steps.Validation.Interfaces;
using DotvvmAcademy.Steps.Validation.Validators.CommonValidators;
using DotVVM.Framework.Configuration;
using DotVVM.Framework.ViewModel;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace DotvvmAcademy.Steps
{
    public class CodeStepCsharp : CodeStepBase<ICSharpCodeValidationObject>
    {
        public override ICSharpCodeValidationObject Validator { get; set; }

        [Bind(Direction.None)]
        public List<string> AllowedTypesConstructed { get; private set; } = new List<string>();

        [Bind(Direction.None)]
        public List<string> AllowedMethodsCalled { get; private set; } = new List<string>();

        [Bind(Direction.None)]
        public List<string> ReferencedAssemblies { get; } = new List<string>
        {
            GetAssemblyLocationFromType(typeof(object)),
            GetAssemblyLocationFromType(typeof(DotvvmConfiguration)),
            GetAssemblyLocationFromType(typeof(BindAttribute)),
            GetAssemblyLocationFromType(typeof(RequiredAttribute)),
            GetAssemblyLocationFromType(typeof(System.Runtime.GCSettings)),
            GetAssemblyLocationFromType(typeof(Attribute)),
            GetAssemblyLocationFromType(typeof(ExtensionAttribute)),
            Path.Combine(Directory.GetCurrentDirectory(), @"libs\System.Runtime.dll")
        };


        private static string GetAssemblyLocationFromType(Type type)
        {
            return type.GetTypeInfo().Assembly.Location;
        }



        [Bind(Direction.None)]
        public List<string> OtherCodeDependencies { get; } = new List<string>();

        protected override IEnumerable<string> GetErrors()
        {
            try
            {
                var tree = (CSharpSyntaxTree) CSharpSyntaxTree.ParseText(Code);

                PortableExecutableReference[] portableExecutableReferences = ReferencedAssemblies.Select(path => MetadataReference.CreateFromFile(path)).ToArray();
                IEnumerable<SyntaxTree> syntaxTrees = new[] {tree}.Concat(OtherCodeDependencies.Select(c => CSharpSyntaxTree.ParseText(c)));

                CSharpCompilation compilation = CSharpCompilation.Create(
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
                return new[] {ex.Message};
            }
        }
    }
}