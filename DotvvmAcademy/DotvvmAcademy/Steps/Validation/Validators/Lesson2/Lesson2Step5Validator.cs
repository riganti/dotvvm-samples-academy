using System.Collections.Generic;
using System.Reflection;
using DotvvmAcademy.Steps.Validation.Interfaces;
using DotvvmAcademy.Steps.Validation.ValidatorProvision;
using DotvvmAcademy.Steps.Validation.Validators.CommonValidators;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace DotvvmAcademy.Steps.Validation.Validators.Lesson2
{
    [StepValidation(ValidatorKey = "Lesson2Step5Validator")]
    public class Lesson2Step5Validator : ICSharpCodeValidationObject
    {
        public void Validate(CSharpCompilation compilation, CSharpSyntaxTree tree, SemanticModel model,
            Assembly assembly)
        {
            var classTreeDeclarations = CsharpCommonValidator.GetClassDeclarations(tree, model);

            var className = "TaskData";
            CsharpCommonValidator.GetClassValidationError(classTreeDeclarations, className);

            var treeProperties = CsharpCommonValidator.GetTreeProperties(tree, model);
            var propertiesToValidate = new List<Property>
            {
                new Property("Title", "string"),
                new Property("IsCompleted", "bool"),
                new Property("Result", "int")
            };

            CsharpCommonValidator.GetPropertiesValidationErrors(treeProperties, propertiesToValidate);
        }
    }
}