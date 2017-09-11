using DotVVM.Framework.Controls;
using DotvvmAcademy.Validation;
using DotvvmAcademy.Validation.CSharp;
using DotvvmAcademy.Validation.Dothtml;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

namespace DotvvmAcademy.Validators
{
    [ValidatorClass("DotvvmAcademy.Lessons.Collections")]
    public static class CollectionsValidators
    {
        [Validator(nameof(Lesson2Step10Validator))]
        public static DothtmlControl Lesson2Step10Validator(DothtmlValidate validate)
        {
            var div = Lesson2Step9Validator(validate);
            var link = div.Control<LinkButton>();
            link.Property(HtmlGenericControl.VisibleProperty).ValueBinding("!IsCompleted");
            return div;
        }

        [Validator(nameof(Lesson2Step11Validator))]
        public static void Lesson2Step11Validator(CSharpValidate validate)
        {
            var viewModel = Lesson2Step7Validator(validate);
            var taskData = validate.Class("DotvvmAcademy.Tutorial.ViewModels.TaskData");
            var completeTask = viewModel.Method("CompleteTask", null, taskData.GetDescriptor());
            var viewModelInstance = viewModel.Instance();
            var taskDataInstance = taskData.Instance();
            viewModelInstance.MethodCall(completeTask, null, taskDataInstance.RawInstance);
            taskDataInstance.PropertyGetter(taskData.Property("IsCompleted"), true);
        }

        [Validator(nameof(Lesson2Step12Validator))]
        public static DothtmlControl Lesson2Step12Validator(DothtmlValidate validate)
        {
            var div = Lesson2Step10Validator(validate);
            var link = div.Control<LinkButton>();
            link.Property(ButtonBase.ClickProperty).CommandBinding("_parent.CompleteTask(_this)", "_root.CompleteTask(_this)");
            return div;
        }

        [Validator(nameof(Lesson2Step13Validator))]
        public static void Lesson2Step13Validator(DothtmlValidate validate)
        {
            var div = Lesson2Step10Validator(validate);
            div.Attribute("class").ValueBinding("{value: IsCompleted ? 'task-completed' : 'task'}");
        }

        [Validator(nameof(Lesson2Step2Validator))]
        public static (DothtmlControl textBox, DothtmlControl button) Lesson2Step2Validator(DothtmlValidate validate)
        {
            var elements = validate.Root.Elements(2);
            elements[0].Tag("p");
            elements[1].Tag("div");
            var textBox = elements[0].Control<TextBox>();
            var button = elements[0].Control<Button>();
            button.Property(ButtonBase.TextProperty).HardcodedValue("Add Task", "add task");
            return (textBox, button);
        }

        [Validator(nameof(Lesson2Step3Validator))]
        public static CSharpClass Lesson2Step3Validator(CSharpValidate validate, params string[] additionalUsings)
        {
            var usings = additionalUsings == null ? new List<string>() : additionalUsings.ToList();
            usings.Add("System");
            validate.Root.Usings(usings);
            var viewModel = validate.Root.Namespace("DotvvmAcademy.Tutorial.ViewModels").Class("Lesson2ViewModel");
            viewModel.AutoProperty<string>("AddedTaskTitle");
            viewModel.Method("AddTask");
            return viewModel;
        }

        [Validator(nameof(Lesson2Step4Validator))]
        public static void Lesson2Step4Validator(DothtmlValidate validate)
        {
            (var textBox, var button) = Lesson2Step2Validator(validate);
            textBox.Property(TextBox.TextProperty).ValueBinding("AddedTaskTitle");
            button.Property(ButtonBase.ClickProperty).CommandBinding("AddTask()");
        }

        [Validator(nameof(Lesson2Step5Validator))]
        public static void Lesson2Step5Validator(CSharpValidate validate)
        {
            validate.Root.Usings("System");
            var taskData = validate.Root.Namespace("DotvvmAcademy.Tutorial.ViewModels").Class("TaskData");
            taskData.AutoProperty<string>("Title");
            taskData.AutoProperty<bool>("IsCompleted");
        }

        [Validator(nameof(Lesson2Step6Validator))]
        public static CSharpClass Lesson2Step6Validator(CSharpValidate validate)
        {
            var viewModel = Lesson2Step3Validator(validate, "System.Collections.Generic");
            var taskDataType = validate.Descriptor("DotvvmAcademy.Tutorial.ViewModels.TaskData");
            var tasks = viewModel.AutoProperty(typeof(List<>).GetDescriptor(validate, taskDataType), "Tasks");
            viewModel.Instance().PropertyGetter(tasks, o =>
            {
                if (o != null && o is IList list && list.Count == 0)
                {
                    return true;
                }
                return false;
            }, o => "The 'Tasks' property should be initialized.");
            return viewModel;
        }

        [Validator(nameof(Lesson2Step7Validator))]
        public static CSharpClass Lesson2Step7Validator(CSharpValidate validate)
        {
            var viewModel = Lesson2Step6Validator(validate);
            var addedTaskTitle = viewModel.Property<string>("AddedTaskTitle");
            var taskDataType = validate.Descriptor("DotvvmAcademy.Tutorial.ViewModels.TaskData");
            var tasks = viewModel.AutoProperty(typeof(List<>).GetDescriptor(validate, taskDataType), "Tasks");
            var instance = viewModel.Instance();

            instance.PropertySetter(addedTaskTitle, validate.TestingValue);
            instance.MethodCall(viewModel.Method("AddTask"));
            instance.PropertyGetter(addedTaskTitle, "", "The 'AddedTaskTitle' property should contain an empty string.");
            instance.PropertyGetter(tasks, o =>
            {
                if (o != null && o is IList list && list.Count == 1)
                {
                    return ((dynamic)list[0]).Title == validate.TestingValue;
                }
                return false;
            }, o => "The 'Tasks' property doesn't contain a 'TaskData' object with its 'Title' property set to the correct value.");

            return viewModel;
        }

        [Validator(nameof(Lesson2Step8Validator))]
        public static DothtmlControl Lesson2Step8Validator(DothtmlValidate validate)
        {
            Lesson2Step4Validator(validate);
            var repeater = validate.Root.Elements()[1].Control<Repeater>();
            repeater.Property(ItemsControl.DataSourceProperty).ValueBinding("Tasks");
            var itemTemplate = repeater.Property(Repeater.ItemTemplateProperty).TemplateContent();
            var div = itemTemplate.Element("div");
            div.Attribute("class").HardcodedValue("task");
            return div;
        }

        [Validator(nameof(Lesson2Step9Validator))]
        public static DothtmlControl Lesson2Step9Validator(DothtmlValidate validate)
        {
            var div = Lesson2Step8Validator(validate);
            var literal = div.Control<Literal>();
            literal.Property(Literal.TextProperty).ValueBinding("Title");
            var link = div.Control<LinkButton>();
            return div;
        }
    }
}