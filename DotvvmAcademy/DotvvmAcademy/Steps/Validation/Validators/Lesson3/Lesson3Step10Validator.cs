using System.Reflection;
using DotvvmAcademy.Steps.Validation.Interfaces;
using DotvvmAcademy.Steps.Validation.ValidatorProvision;
using DotvvmAcademy.Steps.Validation.Validators.CommonValidators;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace DotvvmAcademy.Steps.Validation.Validators.Lesson3
{
	[StepValidation(ValidatorKey = "Lesson3Step10Validator")]
	public class Lesson3Step10Validator : ICSharpCodeValidationObject
	{
		public void Validate(CSharpCompilation compilation, CSharpSyntaxTree tree, SemanticModel model, Assembly assembly)
		{

			CSharpCommonValidator.ValidateProperties(tree, model, Lesson3CommonValidator.CreateStep9Properties());


			ValidatorsExtensions.ExecuteSafe(() =>
			{
				var viewModel = (dynamic)assembly.CreateInstance("DotvvmAcademy.Tutorial.ViewModels.Lesson3ViewModel");
				
				var usa = viewModel?.Countries[0];
				if (usa == null ||  usa.Name != "USA" || usa.Id != 1)
				{
					throw new CodeValidationException(string.Format(Lesson3Texts.CountryInfoError, "USA", 1));
				}
				var canada = viewModel?.Countries[1];
				if (canada == null || canada.Name != "Canada" || canada.Id != 2)
				{
					throw new CodeValidationException(string.Format(Lesson3Texts.CountryInfoError, "Canada", 2));
				}
			});
		}
	}
}