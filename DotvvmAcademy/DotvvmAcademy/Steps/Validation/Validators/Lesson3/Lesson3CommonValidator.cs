using System.Collections.Generic;
using DotvvmAcademy.Steps.Validation.Validators.CommonValidators;
using DotVVM.Framework.Compilation.ControlTree.Resolved;
using DotVVM.Framework.Controls;

namespace DotvvmAcademy.Steps.Validation.Validators.Lesson3
{
    public class Lesson3CommonValidator
    {
        public static List<Property> CreateStep2Properties()
        {
            return new List<Property>
            {
                new Property("FirstName", "string"),
                new Property("LastName", "string")
            };
        }

        public static List<Property> CreateStep4Properties()
        {
            var properties = CreateStep2Properties();
            properties.Add(new Property("Role", "string"));
            return properties;
        }
        public static List<Property> CreateStep6Properties()
        {
            var properties = CreateStep4Properties();
            properties.Add(new Property("Interests", "System.Collections.Generic.List<string>"));
            return properties;
        }

        public static void Step3Validator(ResolvedTreeRoot resolvedTreeRoot)
        {
            ValidationExtensions.CheckTypeAndCount<TextBox>(resolvedTreeRoot, 2);
            DotHtmlCommonValidator.ValidatePropertiesBindings(resolvedTreeRoot, CreateStep2Properties());
        }

        public static List<Property> CreateStep8Properties()
        {
            return new List<Property>
            {
                new Property("Id", "int"),
                new Property("Name", "string")
            };
        }
    }
}