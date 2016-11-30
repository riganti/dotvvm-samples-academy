using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using DotvvmAcademy.Steps.Validation.Validators.CommonValidators;
using DotvvmAcademy.Steps.Validation.Validators.PropertyAndControlType;
using DotVVM.Framework.Compilation.ControlTree.Resolved;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace DotvvmAcademy.Steps.Validation.Validators.Lesson4
{
    public class Lesson4CommonValidator
    {
        public static List<Property> CreateStep2RequiredProperties()
        {
            return new List<Property>
            {
                new Property("City", "string", ControlBindName.TextBoxText),
                new Property("ZIP", "string", ControlBindName.TextBoxText)
            };
        }

        public static Property CreateStep2EmailAddressProperty()
        {
            return new Property("Email", "string", ControlBindName.TextBoxText);
        }

        public static List<Property> CreateStep2ControlProperties()
        {
            var properties = CreateStep2RequiredProperties();
            properties.Add(CreateStep2EmailAddressProperty());
            return properties;
        }
        public static List<Property> CreateStep2ValidationValueProperties()
        {
            var properties = CreateStep2ControlProperties();

            foreach (var property in properties)
            {
                property.TargetControlBindName = ControlBindName.DivValidationValue;
            }
            return properties;
        }


        public static void ValidateStep2Properties(CSharpSyntaxTree tree, SemanticModel model, Assembly assembly)
        {
           

            CSharpCommonValidator.ValidateProperties(tree, model, CreateStep2ControlProperties());

            
            ValidationExtensions.ExecuteSafe(() =>
            {

                var viewModel = (dynamic)assembly.CreateInstance("DotvvmAcademy.Tutorial.ViewModels.Lesson4ViewModel");

                var modelProperties = (viewModel.GetType().GetProperties() as IEnumerable<PropertyInfo>).ToList();

                foreach (var requiredProperty in CreateStep2RequiredProperties())
                {
                    var viewModelProperty = modelProperties.FirstOrDefault(a=> a.Name == requiredProperty.Name);

                    if (viewModelProperty == null)
                    {
                        throw new CodeValidationException(string.Format(ValidationErrorMessages.PropertyNotFound, requiredProperty.Name));
                    }

                    if (!viewModelProperty.IsDefined(typeof(RequiredAttribute)))
                    {
                        throw new CodeValidationException(string.Format(Lesson4Texts.AttributeMissing,
                            requiredProperty.Name, nameof(RequiredAttribute)));
                    }
                }
                var emailPropertyName = CreateStep2EmailAddressProperty().Name;

                var emailProperty = modelProperties.FirstOrDefault(a => a.Name == emailPropertyName);
                if (emailProperty == null)
                {
                    throw new CodeValidationException(string.Format(ValidationErrorMessages.PropertyNotFound, emailPropertyName));
                }
                if (!emailProperty.IsDefined(typeof(EmailAddressAttribute)))
                {
                    throw new CodeValidationException(string.Format(Lesson4Texts.AttributeMissing,
                        emailPropertyName, nameof(EmailAddressAttribute)));
                }
            });
        }

        public static void ValidateStep3Properties(ResolvedTreeRoot resolvedTreeRoot)
        {
            DotHtmlCommonValidator.CheckTypeAndCountHtmlTag(resolvedTreeRoot, "div", 3);
            DotHtmlCommonValidator.ValidatePropertiesBindings(resolvedTreeRoot, CreateStep2ControlProperties());
            DotHtmlCommonValidator.ValidatePropertiesBindings(resolvedTreeRoot, CreateStep2ValidationValueProperties());
            
        }

    }
}