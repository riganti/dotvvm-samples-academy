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
                new Property("Number1", "int"),
                new Property("Number2", "int"),
                new Property("Result", "int")
            };
        }

        public static void ValidateTextBoxBindings(ResolvedTreeRoot resolvedTreeRoot)
        {
            ValidateBasicControls(resolvedTreeRoot);
            DotHtmlCommonValidator.ValidatePropertiesBindings(resolvedTreeRoot, CreateStep4Properties());
        }

        public static void ValidateBasicControls(ResolvedTreeRoot resolvedTreeRoot)
        {
            ValidationExtensions.CheckTypeAndCount<TextBox>(resolvedTreeRoot, 3);
            ValidationExtensions.CheckTypeAndCount<Button>(resolvedTreeRoot, 1);
        }
    }
}