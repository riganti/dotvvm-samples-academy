using System.Collections.Generic;
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
            Lesson1CommonValidator.ValidateViewModelProperties(compilation, tree, model, assembly);

            var methods = CsharpCommonValidator.GetTreeMethods(tree, model);

            var methodName = "AddTask";
            CsharpCommonValidator.GetVoidMethodValidationError(methods, methodName);

            this.ExecuteSafe(() =>
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