using DotvvmAcademy.Steps.Validation.Interfaces;
using DotvvmAcademy.Steps.Validation.ValidatorProvision;
using DotvvmAcademy.Steps.Validation.Validators.CommonValidators;
using DotvvmAcademy.Steps.Validation.Validators.PropertyAndControlType;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System.Collections.Generic;
using System.Reflection;

namespace DotvvmAcademy.Steps.Validation.Validators.Lesson2
{
    [StepValidation(ValidatorKey = "Lesson2Step5Validator")]
    public class Lesson2Step5Validator : ICSharpCodeValidationObject
    {
        public void Validate(CSharpCompilation compilation, CSharpSyntaxTree tree, SemanticModel model,
            Assembly assembly)
        {
            var taskdata = "TaskData";
            CSharpCommonValidator.ValidateClass(tree, model, taskdata);

            List<Property> propertiesToValidate = Lesson2CommonValidator.CreateStep5Properties();
            CSharpCommonValidator.ValidateProperties(tree, model, propertiesToValidate);
        }
    }
}