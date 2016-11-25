using System.Collections.Generic;
using System.Reflection;
using DotvvmAcademy.Steps.Validation.Interfaces;
using DotvvmAcademy.Steps.Validation.ValidatorProvision;
using DotvvmAcademy.Steps.Validation.Validators.CommonValidators;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace DotvvmAcademy.Steps.Validation.Validators.Lesson3
{
    [StepValidation(ValidatorKey = "Lesson3Step8Validator")]
    public class Lesson3Step8Validator : ICSharpCodeValidationObject
    {
        public void Validate(CSharpCompilation compilation, CSharpSyntaxTree tree, SemanticModel model, Assembly assembly)
        {
            var className = "CountryInfo";
            CSharpCommonValidator.ValidateClass(tree,model,className);
            CSharpCommonValidator.ValidateProperties(tree,model, CreateStep8Properties());
        }

        private static List<Property> CreateStep8Properties()
        {
            return new List<Property>
            {
                new Property("Id", "int", ControlBindName.NotExist),
                new Property("Name", "string", ControlBindName.NotExist)
            };
        }
    }
}