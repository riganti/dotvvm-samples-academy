using System.Reflection;
using DotvvmAcademy.Steps.Validation.Interfaces;
using DotvvmAcademy.Steps.Validation.ValidatorProvision;
using DotvvmAcademy.Steps.Validation.Validators.CommonValidators;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace DotvvmAcademy.Steps.Validation.Validators.Lesson3
{
    [StepValidation(ValidatorKey = "Lesson3Step4Validator")]
    public class Lesson3Step4Validator : ICSharpCodeValidationObject
    {
        public void Validate(CSharpCompilation compilation, CSharpSyntaxTree tree, SemanticModel model,
            Assembly assembly)
        {
            var propertiesToValidate = Lesson3CommonValidator.CreateStep4Properties();

            CsharpCommonValidator.ValidateProperties(tree, model, propertiesToValidate);
        }
    }
}