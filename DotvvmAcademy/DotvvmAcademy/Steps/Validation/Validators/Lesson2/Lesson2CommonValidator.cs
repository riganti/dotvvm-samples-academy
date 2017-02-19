using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DotvvmAcademy.Steps.Validation.Validators.CommonValidators;
using DotvvmAcademy.Steps.Validation.Validators.PropertyAndControlType;
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
			return new Property("AddedTaskTitle", "string", ControlBindName.TextBoxText);
		}

		public static Property CreateStep6Property()
		{
			return new Property("Tasks",
				"System.Collections.Generic.List<DotvvmAcademy.Tutorial.ViewModels.TaskData>",
				ControlBindName.RepeaterDataSource);
		}

		public static void ValidateAddTaskProperties(CSharpCompilation compilation, CSharpSyntaxTree tree,
			SemanticModel model, Assembly assembly)
		{
			CSharpCommonValidator.ValidateProperty(tree, model, CreateStep3Property());

			var methodName = "AddTask";
			CSharpCommonValidator.ValidateMethod(tree, model, methodName);
		}


		public static void ValidateTasksProperty(CSharpCompilation compilation, CSharpSyntaxTree tree,
			SemanticModel model, Assembly assembly)
		{
			ValidateAddTaskProperties(compilation, tree, model, assembly);
			CSharpCommonValidator.ValidateProperty(tree, model, CreateStep6Property());
		}


		public static void ValidateAddTaskMethod(CSharpCompilation compilation, CSharpSyntaxTree tree,
			SemanticModel model, Assembly assembly)
		{
			ValidateTasksProperty(compilation, tree, model, assembly);

			ValidatorsExtensions.ExecuteSafe(() =>
			{
				var viewModel = (dynamic) assembly.CreateInstance("DotvvmAcademy.Tutorial.ViewModels.Lesson2ViewModel");
				viewModel.AddedTaskTitle = "test";
				viewModel.AddTask();

				if (viewModel.Tasks.Count != 1)
					throw new CodeValidationException(Lesson2Texts.AddTaskShouldAddTaskObject);
				if (viewModel.Tasks[0].Title != "test")
					throw new CodeValidationException(Lesson2Texts.AddTaskInvalidTaskText);
				if (viewModel.AddedTaskTitle != "")
					throw new CodeValidationException(Lesson2Texts.AddTaskTitleNotReset);
			});
		}

		public static void ValidateAddTaskControls(ResolvedTreeRoot resolvedTreeRoot)
		{
			DotHtmlCommonValidator.CheckControlTypeCount<TextBox>(resolvedTreeRoot, 1);
			DotHtmlCommonValidator.CheckControlTypeCount<Button>(resolvedTreeRoot, 1);


			var buttonText = resolvedTreeRoot.GetDescendantControls<Button>()
				.Select(c => c.GetValue(ButtonBase.TextProperty))
				.SingleOrDefault();

			if (buttonText == null)
				throw new CodeValidationException(Lesson2Texts.ButtonDoesNotHaveText);
		}

		public static void ValidateRepeaterTemplate3(ResolvedTreeRoot root)
		{
			ValidateRepeaterTemplate2(root);

			var linkButton = DotHtmlCommonValidator.GetControlInRepeater<LinkButton>(root);


			linkButton.ValidateCommandBindingExpression(ButtonBase.ClickProperty, "_parent.CompleteTask(_this)");
		}


		public static void ValidateRepeaterTemplate2(ResolvedTreeRoot root)
		{
			ValidateRepeaterTemplate1(root);
			var linkButton = DotHtmlCommonValidator.GetControlInRepeater<LinkButton>(root);
			if (linkButton.GetValueBindingText(HtmlGenericControl.VisibleProperty) != "!IsCompleted")
				throw new CodeValidationException(Lesson2Texts.InvalidVisibleBinding);
		}

		public static void ValidateRepeaterTemplate1(ResolvedTreeRoot root)
		{
			ValidateRepeaterControl(root);

			DotHtmlCommonValidator.ValidatePropertyBinding(root, CreateStep5TitleProperty());
			DotHtmlCommonValidator.CheckControlTypeCountInRepeater<LinkButton>(root, 1);
		}

		public static List<Property> CreateStep5Properties()
		{
			return new List<Property>
			{
				CreateStep5TitleProperty(),
				new Property("IsCompleted", "bool", ControlBindName.DivClass)
			};
		}

		public static Property CreateStep5TitleProperty()
		{
			return new Property("Title", "string", ControlBindName.RepeaterLiteral);
		}


		public static void ValidateRepeaterControl(ResolvedTreeRoot resolvedTreeRoot)
		{
			ValidateAddTaskControlBindings(resolvedTreeRoot);

			DotHtmlCommonValidator.CheckControlTypeCount<Repeater>(resolvedTreeRoot, 1);


			var repeater = resolvedTreeRoot.GetDescendantControls<Repeater>().Single();

			DotHtmlCommonValidator.ValidatePropertyBinding(resolvedTreeRoot, CreateStep6Property());


			DotHtmlCommonValidator.CheckCountOfHtmlTag(resolvedTreeRoot, "div", 1);

			IAbstractPropertySetter setter;
			if (!repeater.TryGetProperty(Repeater.ItemTemplateProperty, out setter)
			    || !(setter is ResolvedPropertyTemplate))
				throw new CodeValidationException(Lesson2Texts.RepeaterTemplateMissingDivError);
			var template = (ResolvedPropertyTemplate) setter;

			var div = template.GetDescendantControls<HtmlGenericControl>().ToList();
			if (div.Count(d => d.DothtmlNode.As<DothtmlElementNode>()?.TagName == "div") != 1)
				throw new CodeValidationException(Lesson2Texts.RepeaterTemplateMissingDivError);
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