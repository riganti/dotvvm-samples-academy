using System.Collections.Generic;
using System.Reflection;
using DotvvmAcademy.Steps.Validation.Validators.CommonValidators;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace DotvvmAcademy.Steps.Validation.Validators.Lesson1
{
    public class Lesson1CommonValidator
    {
        public static void ValidateViewModelProperties(CSharpCompilation compilation, CSharpSyntaxTree tree,
           SemanticModel model, Assembly assembly)
        {
            var properties = CsharpCommonValidator.GetTreeProperties(tree, model);
            var propertiesToValidate = new List<Property>
            {
                new Property("Number1", "int"),
                new Property("Number2", "int"),
                new Property("Result", "int")
            };

            CsharpCommonValidator.GetPropertiesValidationErrors(properties, propertiesToValidate);
        }
    }
}