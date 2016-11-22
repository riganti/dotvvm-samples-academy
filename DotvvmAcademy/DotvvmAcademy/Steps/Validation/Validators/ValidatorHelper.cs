using System.Linq;
using System.Reflection;
using DotvvmAcademy.Steps.Validation.Interfaces;
using DotvvmAcademy.Steps.Validation.Validators.Lesson1;
using DotvvmAcademy.Steps.Validation.Validators.Lesson2;
using DotVVM.Framework.Compilation.ControlTree;
using DotVVM.Framework.Compilation.ControlTree.Resolved;
using DotVVM.Framework.Compilation.Parser.Dothtml.Parser;
using DotVVM.Framework.Controls;
using DotVVM.Framework.Utils;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace DotvvmAcademy.Steps.Validation.Validators
{
    public static class ValidatorHelper
    {
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

        public static void ValidateViewModelProperties(CSharpCompilation compilation, CSharpSyntaxTree tree,
            SemanticModel model, Assembly assembly)
        {
            var properties = tree.GetCompilationUnitRoot().DescendantNodes().OfType<PropertyDeclarationSyntax>()
                .Select(p => model.GetDeclaredSymbol(p))
                .ToList();

            if (properties.Count(p => p.CheckNameAndType("Number1", "int")) != 1)
            {
                throw new CodeValidationException(string.Format(ValidationErrorMessages.PropertyNotFound, "Number1"));
            }
            if (properties.Count(p => p.CheckNameAndType("Number2", "int")) != 1)
            {
                throw new CodeValidationException(string.Format(ValidationErrorMessages.PropertyNotFound, "Number2"));
            }
            if (properties.Count(p => p.CheckNameAndType("Result", "int")) != 1)
            {
                throw new CodeValidationException(string.Format(ValidationErrorMessages.PropertyNotFound, "Result"));
            }
        }

        public static void ValidateTextBoxBindings(ResolvedTreeRoot root)
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

        public static void ValidateAddTaskControls(ResolvedTreeRoot root)
        {
            if (root.GetDescendantControls<TextBox>().Count() != 1)
            {
                throw new CodeValidationException(Lesson2Texts.AddTaskTextBoxControlError);
            }
            if (root.GetDescendantControls<Button>().Count() != 1)
            {
                throw new CodeValidationException(Lesson2Texts.AddTaskButtonControlError);
            }

            var buttonTextBinding = root.GetDescendantControls<Button>()
                .Select(c => c.GetValue(ButtonBase.TextProperty))
                .SingleOrDefault();
            if (buttonTextBinding == null)
            {
                throw new CodeValidationException(Lesson2Texts.ButtonDoesNotHaveText);
            }
        }

        public static void ValidateAddTaskProperties(CSharpCompilation compilation, CSharpSyntaxTree tree,
            SemanticModel model, Assembly assembly)
        {
            var properties = tree.GetCompilationUnitRoot().DescendantNodes().OfType<PropertyDeclarationSyntax>()
                .Select(p => model.GetDeclaredSymbol(p))
                .ToList();

            if (properties.Count(p => p.CheckNameAndType("AddedTaskTitle", "string")) != 1)
            {
                throw new CodeValidationException(string.Format(ValidationErrorMessages.PropertyNotFound,
                    "AddedTaskTitle"));
            }

            var methods = tree.GetCompilationUnitRoot().DescendantNodes().OfType<MethodDeclarationSyntax>()
                .Select(p => model.GetDeclaredSymbol(p))
                .ToList();

            if (methods.Count(m => m.CheckNameAndVoid("AddTask")) != 1)
            {
                throw new CodeValidationException(string.Format(ValidationErrorMessages.MethodNotFound,
                    "Calculate"));
            }
        }

        public static void ValidateAddTaskControlBindings(ResolvedTreeRoot root)
        {
            ValidateAddTaskControls(root);

            var propertyBindings = root.GetDescendantControls<TextBox>()
                .Select(c => c.GetValueBindingText(TextBox.TextProperty))
                .ToList();
            if (!propertyBindings.Contains("AddedTaskTitle"))
            {
                throw new CodeValidationException(Lesson2Texts.TextBoxBindingError);
            }

            root.GetDescendantControls<Button>().Single()
                .ValidateCommandBindingExpression(ButtonBase.ClickProperty, "AddTask()");
        }


        public static void ValidateTasksProperty(CSharpCompilation compilation, CSharpSyntaxTree tree,
            SemanticModel model, Assembly assembly)
        {
            ValidateAddTaskProperties(compilation, tree, model, assembly);

            var properties = tree.GetCompilationUnitRoot().DescendantNodes().OfType<PropertyDeclarationSyntax>()
                .Select(p => model.GetDeclaredSymbol(p))
                .ToList();
            if (
                properties.Count(
                    p =>
                        p.CheckNameAndType("Tasks",
                            "System.Collections.Generic.List<DotvvmAcademy.Tutorial.ViewModels.TaskData>")) != 1)
            {
                throw new CodeValidationException(string.Format(ValidationErrorMessages.PropertyNotFound, "Tasks"));
            }
        }


        public static void ValidateAddTaskMethod(CSharpCompilation compilation, CSharpSyntaxTree tree,
            SemanticModel model, Assembly assembly, ILessonValidationObject validator)
        {
            ValidateTasksProperty(compilation, tree, model, assembly);

            validator.ExecuteSafe(() =>
            {
                var viewModel = (dynamic) assembly.CreateInstance("DotvvmAcademy.Tutorial.ViewModels.Lesson2ViewModel");
                viewModel.AddedTaskTitle = "test";
                viewModel.AddTask();

                if (viewModel.Tasks.Count != 1)
                {
                    throw new CodeValidationException("The AddTask() method should add one task!");
                }
                if (viewModel.Tasks[0].Title != "test")
                {
                    throw new CodeValidationException(
                        "When creating a task, use the AddedTaskTitle as a title of the task!");
                }
                if (viewModel.AddedTaskTitle != "")
                {
                    throw new CodeValidationException(
                        "You need to reset the AddedTaskTitle property to an empty string after you create a task!");
                }
            });
        }

        public static void ValidateRepeaterControl(ResolvedTreeRoot root)
        {
            ValidateAddTaskControlBindings(root);

            if (root.GetDescendantControls<Repeater>().Count() != 1)
            {
                throw new CodeValidationException(Lesson2Texts.RepeaterControlError);
            }

            var repeater = root.GetDescendantControls<Repeater>().Single();
            if (repeater.GetValueBindingText(ItemsControl.DataSourceProperty) != "Tasks")
            {
                throw new CodeValidationException(Lesson2Texts.RepeaterBindingError);
            }

            IAbstractPropertySetter setter;
            if (!repeater.TryGetProperty(Repeater.ItemTemplateProperty, out setter)
                || !(setter is ResolvedPropertyTemplate))
            {
                throw new CodeValidationException(Lesson2Texts.RepeaterTemplateMissingDivError);
            }
            var template = (ResolvedPropertyTemplate) setter;

            var div = template.GetDescendantControls<HtmlGenericControl>().ToList();
            if (div.Count(d => d.DothtmlNode.As<DothtmlElementNode>()?.TagName == "div") != 1)
            {
                throw new CodeValidationException(Lesson2Texts.RepeaterTemplateMissingDivError);
            }
        }

        public static void ValidateRepeaterTemplate1(ResolvedTreeRoot root)
        {
            ValidateRepeaterControl(root);

            var template = root.GetDescendantControls<Repeater>().Single()
                .Properties[Repeater.ItemTemplateProperty]
                .CastTo<ResolvedPropertyTemplate>();

            var literals = template.GetDescendantControls<Literal>()
                .Select(l => l.GetValueBindingOrNull(Literal.TextProperty))
                .Where(l => l != null)
                .ToList();
            if (literals.Count(b => b.Binding.Value == "Title") != 1)
            {
                throw new CodeValidationException(Lesson2Texts.TitleBindingNotFound);
            }

            var linkButtons = template.GetDescendantControls<LinkButton>().ToList();
            if (linkButtons.Count != 1)
            {
                throw new CodeValidationException(Lesson2Texts.LinkButtonNotFound);
            }
        }

        public static void ValidateRepeaterTemplate2(ResolvedTreeRoot root)
        {
            ValidateRepeaterTemplate1(root);
            //todo createTemplate etc!
            var template = root.GetDescendantControls<Repeater>().Single()
                .Properties[Repeater.ItemTemplateProperty]
                .CastTo<ResolvedPropertyTemplate>();

            var linkButton = template
                .GetDescendantControls<LinkButton>()
                .Single();
            if (linkButton.GetValueBindingText(HtmlGenericControl.VisibleProperty) != "!IsCompleted")
            {
                throw new CodeValidationException(Lesson2Texts.InvalidVisibleBinding);
            }
        }

        public static void ValidateRepeaterTemplate3(ResolvedTreeRoot root)
        {
            ValidateRepeaterTemplate2(root);

            var template = root.GetDescendantControls<Repeater>().Single()
                .Properties[Repeater.ItemTemplateProperty]
                .CastTo<ResolvedPropertyTemplate>();

            var linkButton = template
                .GetDescendantControls<LinkButton>()
                .Single();

            linkButton.ValidateCommandBindingExpression(ButtonBase.ClickProperty, "_parent.CompleteTask(_this)");
        }
    }
}