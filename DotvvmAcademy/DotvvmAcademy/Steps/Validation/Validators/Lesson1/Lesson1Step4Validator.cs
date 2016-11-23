using System.Linq;
using System.Reflection;
using DotvvmAcademy.Steps.Validation.Interfaces;
using DotvvmAcademy.Steps.Validation.ValidatorProvision;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System.Collections.Generic;
using DotvvmAcademy.Steps.Validation.Validators.CommonValidators;

namespace DotvvmAcademy.Steps.Validation.Validators.Lesson1
{
    [StepValidation(ValidatorKey = "Lesson1Step4Validator")]
    public class Lesson1Step4ValidationObject : ICSharpCodeValidationObject
    {
        public void Validate(CSharpCompilation compilation, CSharpSyntaxTree tree, SemanticModel model,
            Assembly assembly)
        {
            CsharpCommonValidator.ValidateViewModelProperties(compilation, tree, model, assembly);
        }
    }
}