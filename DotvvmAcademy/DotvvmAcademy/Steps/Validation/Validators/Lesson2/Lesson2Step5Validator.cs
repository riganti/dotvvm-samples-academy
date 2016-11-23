using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DotvvmAcademy.Steps.Validation.Interfaces;
using DotvvmAcademy.Steps.Validation.ValidatorProvision;
using DotvvmAcademy.Steps.Validation.Validators.CommonValidators;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace DotvvmAcademy.Steps.Validation.Validators.Lesson2
{
    [StepValidation(ValidatorKey = "Lesson2Step5Validator")]
    public class Lesson2Step5Validator : ICSharpCodeValidationObject
    {
        public void Validate(CSharpCompilation compilation, CSharpSyntaxTree tree, SemanticModel model,
            Assembly assembly)
        {
            //todo to commonValidator
            var classDeclarations = tree.GetCompilationUnitRoot().DescendantNodes().OfType<ClassDeclarationSyntax>()
                .Select(c => model.GetDeclaredSymbol(c))
                .ToList();
            if (classDeclarations.Count(c => c.Name == "TaskData") != 1)
            {
                throw new CodeValidationException(string.Format(ValidationErrorMessages.ClassNotFound, "TaskData"));
            }



            var properties = CsharpCommonValidator.GetTreeProperties(tree, model);

            var props = new List<Property>
            {
                new Property("Title", "string"),
                new Property("IsCompleted", "bool"),
                new Property("Result", "int")
            };

            CsharpCommonValidator.GetValidationPropertiesErrors(properties, props);
        }
    }
}