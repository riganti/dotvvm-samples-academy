using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using DotvvmAcademy.Steps.Validation.Validators.CommonValidators;
using DotvvmAcademy.Steps.Validation.Validators.PropertyAndControlType;
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
                new Property("City", "string", ControlBindName.NotExist),
                new Property("ZIP", "string", ControlBindName.NotExist)
            };
        }


        public static Property CreateStep2EmailAddressProperty()
        {
            return new Property("Email", "string", ControlBindName.NotExist);
        }

        public static List<Property> CreateStep2Properties()
        {
            var properties = CreateStep2RequiredProperties();
            properties.Add(CreateStep2EmailAddressProperty());
            return properties;
        }

        public static void ValidateStep2Properties(CSharpSyntaxTree tree, SemanticModel model)
        {
            CSharpCommonValidator.ValidateProperties(tree, model, CreateStep2Properties());

            ValidationExtensions.ExecuteSafe(() =>
            {
                foreach (var requiredProperty in CreateStep2RequiredProperties())
                {
                    var modelProperty =
                        model.GetType().GetProperties().FirstOrDefault(a => a.Name == requiredProperty.Name);
                    if (!modelProperty.IsDefined(typeof(RequiredAttribute)))
                    {
                        throw new CodeValidationException(string.Format(Lesson4Texts.RequiredAttributeMissing,
                            requiredProperty.Name));
                    }
                }

                var emailProperty =
                    model.GetType()
                        .GetProperties()
                        .FirstOrDefault(a => a.Name == CreateStep2EmailAddressProperty().Name);
                if (!emailProperty.IsDefined(typeof(EmailAddressAttribute)))
                {
                    throw new CodeValidationException(string.Format(Lesson4Texts.EmailAddressAttributeMissing,
                        emailProperty.Name));
                }
            });
        }
    }
}