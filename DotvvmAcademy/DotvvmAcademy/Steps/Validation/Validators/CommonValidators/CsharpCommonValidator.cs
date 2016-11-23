using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DotvvmAcademy.Steps.Validation.Interfaces;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace DotvvmAcademy.Steps.Validation.Validators.CommonValidators
{
    public class CsharpCommonValidator
    {
        public static void ValidateAddTaskProperties(CSharpCompilation compilation, CSharpSyntaxTree tree,
            SemanticModel model, Assembly assembly)
        {
            var properties = GetTreeProperties(tree, model);

            var props = new List<Property>
            {
                new Property("AddedTaskTitle", "string")
            };

            GetValidationPropertiesErrors(properties, props);


            var methods = GetTreeMethods(tree, model);
            var methodsNames = new List<string>
            {
                "AddTask"
            };
            GetValidationVoidMethodsErrors(methods, methodsNames);
        }


        public static void ValidateTasksProperty(CSharpCompilation compilation, CSharpSyntaxTree tree,
            SemanticModel model, Assembly assembly)
        {
            ValidateAddTaskProperties(compilation, tree, model, assembly);

            var properties = GetTreeProperties(tree, model);

            var props = new List<Property>
            {
                new Property("Tasks", "System.Collections.Generic.List<DotvvmAcademy.Tutorial.ViewModels.TaskData>")
            };


            GetValidationPropertiesErrors(properties, props);
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


        public static void ValidateViewModelProperties(CSharpCompilation compilation, CSharpSyntaxTree tree,
            SemanticModel model, Assembly assembly)
        {
            var properties = GetTreeProperties(tree, model);
            //todo tttt
            var props = new List<Property>
            {
                new Property("Number1", "int"),
                new Property("Number2", "int"),
                new Property("Result", "int")
            };

            GetValidationPropertiesErrors(properties, props);
        }


        public static void GetValidationVoidMethodsErrors(List<IMethodSymbol> voidMethods, List<string> voidMethodsNames)
        {
            foreach (var methodName in voidMethodsNames)
            {
                GetValidationVoidMethodError(voidMethods, methodName);
            }
        }

        public static void GetValidationVoidMethodError(List<IMethodSymbol> voidMethods, string methodName)
        {
            if (voidMethods.Any(m => m.CheckNameAndVoid(methodName)))
            {
                throw new CodeValidationException(string.Format(ValidationErrorMessages.MethodNotFound, methodName));
            }
        }

        public static List<IPropertySymbol> GetTreeProperties(CSharpSyntaxTree tree, SemanticModel model)
        {
            return tree.GetCompilationUnitRoot().DescendantNodes().OfType<PropertyDeclarationSyntax>()
                .Select(p => model.GetDeclaredSymbol(p))
                .ToList();
        }

        public static List<IMethodSymbol> GetTreeMethods(CSharpSyntaxTree tree, SemanticModel model)
        {
            return tree.GetCompilationUnitRoot()
                .DescendantNodes().OfType<MethodDeclarationSyntax>()
                .Select(p => model.GetDeclaredSymbol(p))
                .ToList();
        }

        public static void GetValidationPropertiesErrors(List<IPropertySymbol> treeProperties, List<Property> propertiesToValidate)
        {
            foreach (var prop in propertiesToValidate)
            {
                GetValidationPropertyError(treeProperties, prop);
            }
        }

        public static void GetValidationPropertyError(List<IPropertySymbol> treeProperties, Property propertyToValidate)
        {
            if (!treeProperties.Any(p => p.CheckNameAndType(propertyToValidate.Name, propertyToValidate.Type)))
            {
                throw new CodeValidationException(string.Format(ValidationErrorMessages.PropertyNotFound, propertyToValidate.Name));
            }
        }

    }
}