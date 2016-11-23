using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DotvvmAcademy.Lessons;
using DotvvmAcademy.Steps.Validation.Interfaces;
using DotvvmAcademy.Steps.Validation.ValidatorProvision;
using DotvvmAcademy.Steps.Validation.Validators.CommonValidators;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace DotvvmAcademy.Steps.Validation.Validators.Lesson2
{
    [StepValidation(ValidatorKey = "Lesson2Step11Validator")]
    public class Lesson2Step11Validator : ICSharpCodeValidationObject
    {
        public void Validate(CSharpCompilation compilation, CSharpSyntaxTree tree, SemanticModel model,
            Assembly assembly)
        {
            CsharpCommonValidator.ValidateAddTaskMethod(compilation, tree, model, assembly, this);
            var methods = CsharpCommonValidator.GetTreeMethods(tree, model);
            var methodsNames = new List<string>
            {
                "CompleteTask"
            };

            CsharpCommonValidator.GetValidationVoidMethodsErrors(methods, methodsNames);


            this.ExecuteSafe(() =>
            {
                var viewModel = (dynamic) assembly.CreateInstance("DotvvmAcademy.Tutorial.ViewModels.Lesson2ViewModel");
                var task = (dynamic) assembly.CreateInstance("DotvvmAcademy.Tutorial.ViewModels.TaskData");
                task.Title = "New Task";
                task.IsCompleted = false;
                viewModel.CompleteTask(task);

                if (!task.IsCompleted)
                {
                    throw new CodeValidationException("The CompleteTask() method should set the IsCompleted to true!");
                }
            });
        }
    }
}