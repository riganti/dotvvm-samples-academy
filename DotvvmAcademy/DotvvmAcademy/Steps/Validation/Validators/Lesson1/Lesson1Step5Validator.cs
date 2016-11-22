using System.Linq;
using System.Reflection;
using DotvvmAcademy.Lessons;
using DotvvmAcademy.Steps.Validation.Interfaces;
using DotvvmAcademy.Steps.Validation.ValidatorProvision;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace DotvvmAcademy.Steps.Validation.Validators.Lesson1
{
    [StepValidation(ValidatorKey = "Lesson1Step5Validator")]
    public class Lesson1Step5ValidationObject : ICSharpCodeValidationObject
    {
        public void Validate(CSharpCompilation compilation, CSharpSyntaxTree tree, SemanticModel model,
            Assembly assembly)
        {
            ValidatorHelper.ValidateViewModelProperties(compilation, tree, model, assembly);

            var methods = tree.GetCompilationUnitRoot().DescendantNodes().OfType<MethodDeclarationSyntax>()
                .Select(m => model.GetDeclaredSymbol(m))
                .ToList();
            if (methods.Count(m => m.CheckNameAndVoid("Calculate")) != 1)
            {
                throw new CodeValidationException(string.Format(ValidationErrorMessages.MethodNotFound, "Calculate"));
            }

            this.ExecuteSafe(() =>
            {
                var viewModel = (dynamic) assembly.CreateInstance("DotvvmAcademy.Tutorial.ViewModels.Lesson1ViewModel");
                viewModel.Number1 = 15;
                viewModel.Number2 = 30;
                viewModel.Calculate();

                if (viewModel.Result != 45)
                {
                    throw new CodeValidationException("The Calculate method returns incorrect result!");
                }
            });
        }
    }
}