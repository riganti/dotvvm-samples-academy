using System.Reflection;
using DotvvmAcademy.Steps.Validation.Interfaces;
using DotvvmAcademy.Steps.Validation.ValidatorProvision;
using DotvvmAcademy.Steps.Validation.Validators.CommonValidators;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace DotvvmAcademy.Steps.Validation.Validators.Lesson1
{
    [StepValidation(ValidatorKey = "Lesson1Step5Validator")]
    public class Lesson1Step5ValidationObject : ICSharpCodeValidationObject
    {
        public void Validate(CSharpCompilation compilation, CSharpSyntaxTree tree, SemanticModel model,
            Assembly assembly)
        {
            CsharpCommonValidator.ValidateProperties(tree, model, Lesson1CommonValidator.CreateStep4Properties());

            var methodName = "AddTask";

            CsharpCommonValidator.ValidateMethod(tree,model,methodName);

            ValidationExtensions.ExecuteSafe(() =>
            {
                var viewModel = (dynamic) assembly.CreateInstance("DotvvmAcademy.Tutorial.ViewModels.Lesson1ViewModel");
                viewModel.Number1 = 15;
                viewModel.Number2 = 30;
                viewModel.Calculate();

                if (viewModel.Result != 45)
                {
                    throw new CodeValidationException("The Calculate method calculate incorrect result!");
                }
            });
        }
    }
}