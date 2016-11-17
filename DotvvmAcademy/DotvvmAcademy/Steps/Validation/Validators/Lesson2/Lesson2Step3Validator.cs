using System.Reflection;
using DotvvmAcademy.Steps.Validation.Interfaces;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace DotvvmAcademy.Steps.Validation.Validators.Lesson2
{
    [StepValidation(ValidationKey = "Lesson2Step3Validator")]
    public class Lesson2Step3Validator : ICSharpCodeStepValidationObject
    {
        public void Validate(CSharpCompilation compilation, CSharpSyntaxTree tree, SemanticModel model,
            Assembly assembly)
        {
            ValidatorHelper.ValidateAddTaskProperties(compilation, tree, model, assembly);
        }
    }
}