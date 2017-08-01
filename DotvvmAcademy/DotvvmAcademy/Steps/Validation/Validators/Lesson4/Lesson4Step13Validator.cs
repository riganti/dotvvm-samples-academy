using System;
using DotvvmAcademy.Steps.Validation.Interfaces;
using DotvvmAcademy.Steps.Validation.ValidatorProvision;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace DotvvmAcademy.Steps.Validation.Validators.Lesson4
{
    [StepValidation(ValidatorKey = "Lesson4Step13Validator")]
    public class Lesson4Step13Validator : ICSharpCodeValidationObject
    {
        public void Validate(CSharpCompilation compilation, CSharpSyntaxTree tree, SemanticModel model,
            Assembly assembly)
        {
            Lesson4CommonValidator.ValidateStep12(tree, model, assembly);

            ValidatorsExtensions.ExecuteSafe(() =>
            {
                var viewModel = (dynamic)assembly.CreateInstance("DotvvmAcademy.Tutorial.ViewModels.Lesson4ViewModel");

                var now = DateTime.UtcNow;

                viewModel.SubscriptionFrom = now.AddDays(3);
                viewModel.SubscriptionTo = now;

                IEnumerable<ValidationResult> validationResults = viewModel.Validate(null);
                if (!validationResults.Any())
                {
                    throw new CodeValidationException(Lesson4Texts.IncorrectValidationRule);
                }
                viewModel.SubscriptionFrom = now;
                viewModel.SubscriptionTo = now.AddDays(5);

                validationResults = viewModel.Validate(null);
                if (validationResults.Any())
                {
                    throw new CodeValidationException(Lesson4Texts.IncorrectValidationRule);
                }
            });
        }
    }
}