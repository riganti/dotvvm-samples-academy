using System.Reflection;
using DotvvmAcademy.Steps.Validation.Interfaces;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace DotvvmAcademy.Steps.Validation.Validators.Lesson1
{
    [StepValidation(ValidationKey = "Lesson1Step4Validator")]
    public class Lesson1Step4ValidationObject : ICSharpCodeStepValidationObject
    {
        public void Validate(CSharpCompilation compilation, CSharpSyntaxTree tree, SemanticModel model,
            Assembly assembly)
        {
            ValidatorHelper.ValidateViewModelProperties(compilation, tree, model, assembly);
        }
    }
}