using DotvvmAcademy.Steps.Validation.Interfaces;
using DotvvmAcademy.Steps.Validation.ValidatorProvision;
using DotVVM.Framework.Compilation.ControlTree.Resolved;
using DotVVM.Framework.Controls;
using System.Linq;
using DotvvmAcademy.Steps.Validation.Validators.CommonValidators;
using DotvvmAcademy.Steps.Validation.Validators.PropertyAndControlType;

namespace DotvvmAcademy.Steps.Validation.Validators.Lesson3
{
    [StepValidation(ValidatorKey = "Lesson3Step15Validator")]
    public class Lesson3Step15Validator : IDotHtmlCodeValidationObject
    {
        public void Validate(ResolvedTreeRoot resolvedTreeRoot)
        {
            var step9Properties = Lesson3CommonValidator.CreateStep9Properties();
            var contriesProperty = step9Properties.Single(p => p.TargetControlBindName == ControlBindName.ComboBoxDataSource);
            contriesProperty.Name = $"_parent.{contriesProperty.Name}";
            DotHtmlCommonValidator.ValidatePropertiesBindings(resolvedTreeRoot,step9Properties);
        }
    }
}