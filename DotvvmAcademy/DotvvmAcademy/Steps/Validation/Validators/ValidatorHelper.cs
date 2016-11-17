using System;
using System.Linq;
using System.Reflection;
using DotvvmAcademy.Lessons;
using DotvvmAcademy.Steps.Validation.Interfaces;
using DotVVM.Framework.Compilation.ControlTree.Resolved;
using DotVVM.Framework.Controls;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CSharp.RuntimeBinder;

namespace DotvvmAcademy.Steps.Validation.Validators
{
    public static class ValidatorHelper
    {
        public static void ValidateBasicControls(this ResolvedTreeRoot root)
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

        public static void ValidateViewModelProperties(CSharpCompilation compilation, CSharpSyntaxTree tree,
            SemanticModel model, Assembly assembly)
        {
            var properties = tree.GetCompilationUnitRoot().DescendantNodes().OfType<PropertyDeclarationSyntax>()
                .Select(p => model.GetDeclaredSymbol(p))
                .ToList();

            if (properties.Count(p => p.CheckNameAndType("Number1", "int")) != 1)
            {
                throw new CodeValidationException(string.Format(GenericTexts.PropertyNotFound, "Number1"));
            }
            if (properties.Count(p => p.CheckNameAndType("Number2", "int")) != 1)
            {
                throw new CodeValidationException(string.Format(GenericTexts.PropertyNotFound, "Number2"));
            }
            if (properties.Count(p => p.CheckNameAndType("Result", "int")) != 1)
            {
                throw new CodeValidationException(string.Format(GenericTexts.PropertyNotFound, "Result"));
            }
        }

        public static void ValidateTextBoxBindings(this ResolvedTreeRoot root)
        {
            ValidateBasicControls(root);

            var propertyBindings = root.GetDescendantControls<TextBox>()
                .Select(c => c.GetValueBindingText(TextBox.TextProperty))
                .ToList();

            if (!propertyBindings.Contains("Number1") || !propertyBindings.Contains("Number2") ||
                !propertyBindings.Contains("Result"))
            {
                throw new CodeValidationException(Lesson1Texts.TextBoxBindingsError);
            }
        }


        public static void ExecuteSafe(this ILessonValidationObject validator, Action action)
        {
            try
            {
                action();
            }
            catch (RuntimeBinderException ex)
            {
                throw new CodeValidationException(GenericTexts.CommandMethodError, ex);
            }
        }

        public static TValue GetAttributeValue<TAttribute, TValue>(this Type type,
            Func<TAttribute, TValue> valueSelector)
            where TAttribute : Attribute
        {
            var att = type.GetTypeInfo().GetCustomAttributes(
                typeof(TAttribute), true
            ).FirstOrDefault() as TAttribute;
            if (att != null)
            {
                return valueSelector(att);
            }
            return default(TValue);
        }
    }
}