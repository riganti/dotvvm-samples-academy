using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using DotvvmAcademy.Steps.Validation.Validators.CommonValidators;
using DotvvmAcademy.Steps.Validation.Validators.PropertyAndControlType;
using DotVVM.Framework.Compilation.ControlTree.Resolved;
using DotVVM.Framework.Compilation.Parser.Dothtml.Parser;
using DotVVM.Framework.Controls;
using DotVVM.Framework.Utils;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Validator = DotVVM.Framework.Controls.Validator;

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
            DotHtmlCommonValidator.CheckCountOfHtmlTag(resolvedTreeRoot, "div", 3);
            DotHtmlCommonValidator.ValidatePropertiesBindings(resolvedTreeRoot, CreateStep2ControlProperties());
            DotHtmlCommonValidator.ValidatePropertiesBindings(resolvedTreeRoot, CreateStep2ValidationValueProperties());
            
        }


        public static void ValidateStep5(ResolvedTreeRoot resolvedTreeRoot)
        {
            ValidateStep3Properties(resolvedTreeRoot);

            var divEnwrapException = new CodeValidationException("You shold enwrap div`s with one div");

            DotHtmlCommonValidator.CheckCountOfHtmlTag(resolvedTreeRoot, "div", 1, divEnwrapException);

            var property = new Property("has-error", "none", ControlBindName.DivValidatorInvalidCssClass);
            DotHtmlCommonValidator.ValidatePropertyBinding(resolvedTreeRoot, property);

            property.TargetControlBindName = ControlBindName.DivValidatorInvalidCssClassRemove;
            var contentNode = resolvedTreeRoot.GetDescendantControls<HtmlGenericControl>().
               FirstOrDefault(d => d.DothtmlNode.As<DothtmlElementNode>()?.TagName == "div");

            DotHtmlCommonValidator.CheckCountOfHtmlTag(contentNode, "div", 3);
            DotHtmlCommonValidator.ValidatePropertyBinding(contentNode, property);

            var redundantInvalidCssException = new CodeValidationException("You should delete Validator.InvalidCssClass=\"has-error\" from child elements");

            DotHtmlCommonValidator.CheckCountOfHtmlTagWithPropertyDescriptor(contentNode,"div",0,Validator.InvalidCssClassProperty,redundantInvalidCssException);

        }
    }
}