using System.Collections.Generic;
using DotvvmAcademy.Steps.Validation.Validators.CommonValidators;
using DotVVM.Framework.Compilation.ControlTree.Resolved;
using DotVVM.Framework.Controls;

namespace DotvvmAcademy.Steps.Validation.Validators.Lesson1
{
    public class Lesson1CommonValidator
    {
        public static List<Property> CreateStep4Properties()
        {
            return new List<Property>
            {
                new Property("Number1", "int", ControlBindName.TextBoxText),
                new Property("Number2", "int", ControlBindName.TextBoxText),
                new Property("Result", "int", ControlBindName.TextBoxText)
            };
        }

        public static void ValidateTextBoxBindings(ResolvedTreeRoot resolvedTreeRoot)
        {
            ValidateBasicControls(resolvedTreeRoot);
            DotHtmlCommonValidator.ValidatePropertiesBindings(resolvedTreeRoot, CreateStep4Properties());
        }

        public static void ValidateBasicControls(ResolvedTreeRoot resolvedTreeRoot)
        {
            DotHtmlCommonValidator.CheckTypeAndCount<TextBox>(resolvedTreeRoot, 3);
            DotHtmlCommonValidator.CheckTypeAndCount<Button>(resolvedTreeRoot, 1);
        }
    }
}