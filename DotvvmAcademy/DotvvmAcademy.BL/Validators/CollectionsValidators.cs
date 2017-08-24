using DotVVM.Framework.Controls;
using DotvvmAcademy.BL.Validation;
using DotvvmAcademy.BL.Validation.CSharp;
using DotvvmAcademy.BL.Validation.Dothtml;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using System;

namespace DotvvmAcademy.BL.Validators
{
    public static class CollectionsValidators
    {
        [Validator(nameof(Lesson2Step10Validator))]
        public static void Lesson2Step10Validator(DothtmlValidate validate)
        {
        }

        [Validator(nameof(Lesson2Step11Validator))]
        public static void Lesson2Step11Validator(CSharpValidate validate)
        {
        }

        [Validator(nameof(Lesson2Step12Validator))]
        public static void Lesson2Step12Validator(DothtmlValidate validate)
        {
        }

        [Validator(nameof(Lesson2Step13Validator))]
        public static void Lesson2Step13Validator(DothtmlValidate validate)
        {
        }

        [Validator(nameof(Lesson2Step2Validator))]
        public static (DothtmlControl textBox, DothtmlControl button) Lesson2Step2Validator(DothtmlValidate validate)
        {
            var children = validate.Root.Children(2);
            children[0].Tag("p");
            children[1].Tag("div");
            var textBox = children[0].Control<TextBox>();
            var button = children[0].Control<Button>();
            button.Property(ButtonBase.TextProperty).HardcodedValue("Add Task", "add task");
            return (textBox, button);
        }

        [Validator(nameof(Lesson2Step3Validator))]
        public static void Lesson2Step3Validator(CSharpValidate validate)
        {
            validate.Root.Usings("System");
            ValidateViewModelStep3(validate);
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
            validate.Root.Usings("System", "System.Collections.Generic");
            var viewModel = ValidateViewModelStep3(validate);
            var tasks = viewModel.AutoProperty("System.Collections.Generic.List<DotvvmAcademy.Tutorial.ViewModels.TaskData>", "Tasks");
            viewModel.Instance().PropertyGetter(tasks, o =>
            {
                if (o != null && o is IList list && list.Count == 0)
                {
                    return true;
                }
                return false;
            }, o => "The 'Tasks' proeperty should be initialized.");
            return viewModel;
        }

        [Validator(nameof(Lesson2Step7Validator))]
        public static void Lesson2Step7Validator(CSharpValidate validate)
        {
            var viewModel = Lesson2Step6Validator(validate);
            var addedTaskTitle = viewModel.Property<string>("AddedTaskTitle");
            var tasks = viewModel.AutoProperty("System.Collections.Generic.List<DotvvmAcademy.Tutorial.ViewModels.TaskData>", "Tasks");
            var instance = viewModel.Instance();

            instance.PropertySetter(addedTaskTitle, validate.TestingValue);
            instance.MethodExecution(viewModel.Method("AddTask", typeof(void)));
            instance.PropertyGetter(addedTaskTitle, "", "The 'AddedTaskTitle' property should contain an empty string.");
            instance.PropertyGetter(tasks, o =>
            {
                if (o != null && o is IList list && list.Count == 1)
                {
                    return ((dynamic)list[0]).Title == validate.TestingValue;
                }
                return false;
            }, o => "The 'Tasks' property doesn't contain a 'TaskData' object with its 'Title' property set to the correct value.");
        }

        [Validator(nameof(Lesson2Step8Validator))]
        public static DothtmlControl Lesson2Step8Validator(DothtmlValidate validate)
        {
            Lesson2Step4Validator(validate);
            var repeater = validate.Root.Children()[1].Control<Repeater>();
            repeater.Property(ItemsControl.DataSourceProperty).ValueBinding("Tasks");
            var itemTemplate = repeater.Property(Repeater.ItemTemplateProperty).TemplateContent();
            var div = repeater.Element("div");
            div.Attribute("class", "task");
            return div;
        }

        [Validator(nameof(Lesson2Step9Validator))]
        public static void Lesson2Step9Validator(DothtmlValidate validate)
        {
            var div = Lesson2Step8Validator(validate);
            div.Control<Literal>().Property(Literal.TextProperty).ValueBinding("Title");
        }

        private static CSharpClass ValidateViewModelStep3(CSharpValidate validate)
        {
            var viewModel = validate.Root.Namespace("DotvvmAcademy.Tutorial.ViewModels").Class("Lesson2ViewModel");
            viewModel.AutoProperty<string>("AddedTaskTitle");
            viewModel.Method("AddTask", typeof(void));
            return viewModel;
        }
    }
}