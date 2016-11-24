using System.Linq;
using System.Reflection;
using DotvvmAcademy.Steps.Validation.Interfaces;
using DotvvmAcademy.Steps.Validation.Validators.CommonValidators;
using DotVVM.Framework.Compilation.ControlTree;
using DotVVM.Framework.Compilation.ControlTree.Resolved;
using DotVVM.Framework.Compilation.Parser.Dothtml.Parser;
using DotVVM.Framework.Controls;
using DotVVM.Framework.Utils;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace DotvvmAcademy.Steps.Validation.Validators.Lesson2
{
    public class Lesson2CommonValidator
    {
        public static Property CreateStep3Property()
        {
            return new Property("AddedTaskTitle", "string");
        }

        public static void ValidateAddTaskProperties(CSharpCompilation compilation, CSharpSyntaxTree tree,
            SemanticModel model, Assembly assembly)
        {
            CsharpCommonValidator.ValidateProperty(tree,model, CreateStep3Property());

            var methodName = "AddTask";
            CsharpCommonValidator.ValidateMethod(tree,model,methodName);
        }


        public static void ValidateTasksProperty(CSharpCompilation compilation, CSharpSyntaxTree tree,
            SemanticModel model, Assembly assembly)
        {
            ValidateAddTaskProperties(compilation, tree, model, assembly);
            var propertyToValidate = new Property("Tasks",
                "System.Collections.Generic.List<DotvvmAcademy.Tutorial.ViewModels.TaskData>");
            CsharpCommonValidator.ValidateProperty(tree,model,propertyToValidate);
        }


        public static void ValidateAddTaskMethod(CSharpCompilation compilation, CSharpSyntaxTree tree,
            SemanticModel model, Assembly assembly, ILessonValidationObject validator)
        {
            ValidateTasksProperty(compilation, tree, model, assembly);

            ValidationExtensions.ExecuteSafe(() =>
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

        public static void ValidateAddTaskControls(ResolvedTreeRoot resolvedTreeRoot)
        {
            ValidationExtensions.CheckTypeAndCount<TextBox>(resolvedTreeRoot, 1);
            ValidationExtensions.CheckTypeAndCount<Button>(resolvedTreeRoot, 1);


            var buttonTextBinding = resolvedTreeRoot.GetDescendantControls<Button>()
                .Select(c => c.GetValue(ButtonBase.TextProperty))
                .SingleOrDefault();

            if (buttonTextBinding == null)
            {
                throw new CodeValidationException(Lesson2Texts.ButtonDoesNotHaveText);
            }
        }

        public static void ValidateRepeaterTemplate3(ResolvedTreeRoot root)
        {
            ValidateRepeaterTemplate2(root);
            var linkButton = DotHtmlCommonValidator.GetLinkButton(root);
            linkButton.ValidateCommandBindingExpression(ButtonBase.ClickProperty, "_parent.CompleteTask(_this)");
        }

        public static void ValidateRepeaterTemplate2(ResolvedTreeRoot root)
        {
            ValidateRepeaterTemplate1(root);
            var linkButton = DotHtmlCommonValidator.GetLinkButton(root);

            if (linkButton.GetValueBindingText(HtmlGenericControl.VisibleProperty) != "!IsCompleted")
            {
                throw new CodeValidationException(Lesson2Texts.InvalidVisibleBinding);
            }
        }

        public static void ValidateRepeaterTemplate1(ResolvedTreeRoot root)
        {
            ValidateRepeaterControl(root);

            var template = DotHtmlCommonValidator.GetPropertyTemplate(root);
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

        public static void ValidateRepeaterControl(ResolvedTreeRoot resolvedTreeRoot)
        {
            ValidateAddTaskControlBindings(resolvedTreeRoot);

            ValidationExtensions.CheckTypeAndCount<Repeater>(resolvedTreeRoot, 1);


            var repeater = resolvedTreeRoot.GetDescendantControls<Repeater>().Single();
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

        public static void ValidateAddTaskControlBindings(ResolvedTreeRoot root)
        {
            ValidateAddTaskControls(root);

            DotHtmlCommonValidator.ValidatePropertyBinding(root, CreateStep3Property());

            root.GetDescendantControls<Button>().Single()
                .ValidateCommandBindingExpression(ButtonBase.ClickProperty, "AddTask()");
        }
    }
}