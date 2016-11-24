using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DotvvmAcademy.Steps.Validation.Validators.CommonValidators;
using DotVVM.Framework.Compilation.ControlTree.Resolved;
using DotVVM.Framework.Controls;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace DotvvmAcademy.Steps.Validation.Validators.Lesson1
{
    public class Lesson1CommonValidator
    {
        public static void ValidateViewModelProperties(CSharpCompilation compilation, CSharpSyntaxTree tree,
           SemanticModel model, Assembly assembly)
        {
            var treeProperties = CsharpCommonValidator.GetTreeProperties(tree, model);
            var propertiesToValidate = new List<Property>
            {
                new Property("Number1", "int"),
                new Property("Number2", "int"),
                new Property("Result", "int")
            };

            CsharpCommonValidator.GetPropertiesValidationErrors(treeProperties, propertiesToValidate);
        }

        public static void ValidateTextBoxBindings(ResolvedTreeRoot root)
        {

            ValidateBasicControls(root);
            var propertyBindings = DotHtmlCommonValidator.GetPropertyBindings(root);

            if (propertyBindings == null)
            {
                throw new ArgumentNullException(nameof(propertyBindings));
            }

            if (!propertyBindings.Contains("Number1") || !propertyBindings.Contains("Number2") ||
                !propertyBindings.Contains("Result"))
            {
                throw new CodeValidationException(Lesson1Texts.TextBoxBindingsError);
            }
        }
        public static void ValidateBasicControls(ResolvedTreeRoot root)
        {
            if (root.GetDescendantControls<TextBox>().Count() != 3)
            {
                throw new CodeValidationException(Lesson1Texts.ThreeTextBoxControlsError);
            }
            if (root.GetDescendantControls<Button>().Count() != 1)
            {
                throw new CodeValidationException(Lesson1Texts.OneButtonControlError);
            }
        }
    }
}