using System.Linq;
using System.Reflection;
using DotvvmAcademy.Lessons;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace DotvvmAcademy.Steps.Validation.Validators.Lesson1
{
    [StepValidation(ValidationKey = "Lesson1Step5Validator")]
    public class Lesson1Step5ValidationObject : ICSharpCodeStepValidationObject
    {
        public void ValidationFunction(CSharpCompilation compilation, CSharpSyntaxTree tree, SemanticModel model,
            Assembly assembly)
        {
            ValidatorHelper.ValidateViewModelProperties(compilation, tree, model, assembly);

            var methods = tree.GetCompilationUnitRoot().DescendantNodes().OfType<MethodDeclarationSyntax>()
                .Select(m => model.GetDeclaredSymbol(m))
                .ToList();
            if (methods.Count(m => m.CheckNameAndVoid("Calculate")) != 1)
            {
                throw new CodeValidationException(string.Format(GenericTexts.MethodNotFound, "Calculate"));
            }
            //todo ILessonValidationObject.ExecuteSafe ???
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