using System.Collections.Generic;
using System.Reflection;
using DotvvmAcademy.Steps.Validation.Interfaces;
using DotvvmAcademy.Steps.Validation.Validators.CommonValidators;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace DotvvmAcademy.Steps.Validation.Validators.Lesson2
{
    public class Lesson2CommonValidator
    {
        public static void ValidateAddTaskProperties(CSharpCompilation compilation, CSharpSyntaxTree tree,
            SemanticModel model, Assembly assembly)
        {
            var properties = CsharpCommonValidator.GetTreeProperties(tree, model);

            var props = new List<Property>
            {
                new Property("AddedTaskTitle", "string")
            };

            CsharpCommonValidator.GetPropertiesValidationErrors(properties, props);

            var methods = CsharpCommonValidator.GetTreeMethods(tree, model);

            var methodName = "AddTask";
            CsharpCommonValidator.GetVoidMethodValidationError(methods, methodName);

        }


        public static void ValidateTasksProperty(CSharpCompilation compilation, CSharpSyntaxTree tree,
            SemanticModel model, Assembly assembly)
        {
            ValidateAddTaskProperties(compilation, tree, model, assembly);

            var properties = CsharpCommonValidator.GetTreeProperties(tree, model);

            var props = new List<Property>
            {
                new Property("Tasks", "System.Collections.Generic.List<DotvvmAcademy.Tutorial.ViewModels.TaskData>")
            };


            CsharpCommonValidator.GetPropertiesValidationErrors(properties, props);
        }


        public static void ValidateAddTaskMethod(CSharpCompilation compilation, CSharpSyntaxTree tree,
            SemanticModel model, Assembly assembly, ILessonValidationObject validator)
        {
            ValidateTasksProperty(compilation, tree, model, assembly);

            validator.ExecuteSafe(() =>
            {
                //todo t
                var viewModel = (dynamic) assembly.CreateInstance("DotvvmAcademy.Tutorial.ViewModels.Lesson2ViewModel");
                viewModel.AddedTaskTitle = "test";
                viewModel.AddTask();

                if (viewModel.Tasks.Count != 1)
                {
                    throw new CodeValidationException("The AddTask() method should add one task!");
                }
                if (viewModel.Tasks[0].Title != "test")
                {
                    throw new CodeValidationException(
                        "When creating a task, use the AddedTaskTitle as a title of the task!");
                }
                if (viewModel.AddedTaskTitle != "")
                {
                    throw new CodeValidationException(
                        "You need to reset the AddedTaskTitle property to an empty string after you create a task!");
                }
            });
        }
    }
}