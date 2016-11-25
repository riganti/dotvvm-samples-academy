using System.Reflection;
using DotvvmAcademy.Steps.Validation.Interfaces;
using DotvvmAcademy.Steps.Validation.ValidatorProvision;
using DotvvmAcademy.Steps.Validation.Validators.CommonValidators;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace DotvvmAcademy.Steps.Validation.Validators.Lesson3
{
    [StepValidation(ValidatorKey = "Lesson3Step13Validator")]
    public class Lesson3Step13Validator : ICSharpCodeValidationObject
    {
        public void Validate(CSharpCompilation compilation, CSharpSyntaxTree tree, SemanticModel model,
            Assembly assembly)
        {
            var properties = Lesson3CommonValidator.CreateOnlyStep9Properties();
            properties.Add(Lesson3CommonValidator.CreateNewCustomerProperty());
            CSharpCommonValidator.ValidateProperties(tree, model, properties);
        }


    }
}