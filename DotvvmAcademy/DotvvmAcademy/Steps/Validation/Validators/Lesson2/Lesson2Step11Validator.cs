using System.Linq;
using System.Reflection;
using DotvvmAcademy.Lessons;
using DotvvmAcademy.Steps.Validation.Interfaces;
using DotvvmAcademy.Steps.Validation.ValidatorProvision;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace DotvvmAcademy.Steps.Validation.Validators.Lesson2
{
    [StepValidation(ValidationKey = "Lesson2Step11Validator")]
    public class Lesson2Step11Validator : ICSharpCodeValidationObject
    {
        public void Validate(CSharpCompilation compilation, CSharpSyntaxTree tree, SemanticModel model,
            Assembly assembly)
        {
            ValidatorHelper.ValidateAddTaskMethod(compilation, tree, model, assembly, this);


            var methods = tree.GetCompilationUnitRoot().DescendantNodes().OfType<MethodDeclarationSyntax>()
                .Select(p => model.GetDeclaredSymbol(p))
                .ToList();

            if (methods.Count(m => m.CheckNameAndVoid("CompleteTask")) != 1)
            {
                throw new CodeValidationException(string.Format(ValidationErrorMessages.MethodNotFound, "CompleteTask"));
            }

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