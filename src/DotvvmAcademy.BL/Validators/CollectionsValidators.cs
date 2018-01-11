using DotvvmAcademy.Validation;
using DotvvmAcademy.Validation.CSharp.UnitValidation.Abstractions;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DotvvmAcademy.BL.Validators
{
    [ValidationClass]
    public static class CollectionsValidators
    {
        //[ValidationMethod]
        //public static DothtmlControl Lesson2Step10Validator(DothtmlValidate validate)
        //{
        //    var div = Lesson2Step9Validator(validate);
        //    var link = div.Control<LinkButton>();
        //    link.Property(HtmlGenericControl.VisibleProperty).ValueBinding("!IsCompleted");
        //    return div;
        //}

        //[ValidationMethod]
        //public static void Lesson2Step11Validator(ICSharpDocument document)
        //{
        //    var viewModel = Lesson2Step7Validator(document);
        //    var taskData = document.Class("DotvvmAcademy.Tutorial.ViewModels.TaskData");
        //    var completeTask = viewModel.Method("CompleteTask", null, taskData.GetDescriptor());
        //    var viewModelInstance = viewModel.Instance();
        //    var taskDataInstance = taskData.Instance();
        //    viewModelInstance.MethodCall(completeTask, null, taskDataInstance.RawInstance);
        //    taskDataInstance.PropertyGetter(taskData.Property("IsCompleted"), true);
        //}

        //[ValidationMethod]
        //public static DothtmlControl Lesson2Step12Validator(DothtmlValidate validate)
        //{
        //    var div = Lesson2Step10Validator(validate);
        //    var link = div.Control<LinkButton>();
        //    link.Property(ButtonBase.ClickProperty).CommandBinding("_parent.CompleteTask(_this)", "_root.CompleteTask(_this)");
        //    return div;
        //}

        //[ValidationMethod]
        //public static void Lesson2Step13Validator(DothtmlValidate validate)
        //{
        //    var div = Lesson2Step10Validator(validate);
        //    div.Attribute("class").ValueBinding("{value: IsCompleted ? 'task-completed' : 'task'}");
        //}

        //[ValidationMethod]
        //public static (DothtmlControl textBox, DothtmlControl button) Lesson2Step2Validator(DothtmlValidate validate)
        //{
        //    var elements = validate.Root.Elements(2);
        //    elements[0].Tag("p");
        //    elements[1].Tag("div");
        //    var textBox = elements[0].Control<TextBox>();
        //    var button = elements[0].Control<Button>();
        //    button.Property(ButtonBase.TextProperty).HardcodedValue("Add Task", "add task");
        //    return (textBox, button);
        //}

        //[ValidationMethod]
        //public static CSharpClass Lesson2Step3Validator(CSharpValidate validate, params string[] additionalUsings)
        //{
        //    var usings = additionalUsings == null ? new List<string>() : additionalUsings.ToList();
        //    usings.Add("System");
        //    validate.Root.Usings(usings);
        //    var viewModel = validate.Root.Namespace("DotvvmAcademy.Tutorial.ViewModels").Class("Lesson2ViewModel");
        //    viewModel.AutoProperty<string>("AddedTaskTitle");
        //    viewModel.Method("AddTask");
        //    return viewModel;
        //}

        //[ValidationMethod]
        //public static void Lesson2Step4Validator(DothtmlValidate validate)
        //{
        //    (var textBox, var button) = Lesson2Step2Validator(validate);
        //    textBox.Property(TextBox.TextProperty).ValueBinding("AddedTaskTitle");
        //    button.Property(ButtonBase.ClickProperty).CommandBinding("AddTask()");
        //}

        //[ValidationMethod]
        //public static void Lesson2Step5Validator(CSharpValidate validate)
        //{
        //    validate.Root.Usings("System");
        //    var taskData = validate.Root.Namespace("DotvvmAcademy.Tutorial.ViewModels").Class("TaskData");
        //    taskData.AutoProperty<string>("Title");
        //    taskData.AutoProperty<bool>("IsCompleted");
        //}

        //[ValidationMethod]
        //public static CSharpClass Lesson2Step6Validator(CSharpValidate validate)
        //{
        //    var viewModel = Lesson2Step3Validator(validate, "System.Collections.Generic");
        //    var taskDataType = validate.Descriptor("DotvvmAcademy.Tutorial.ViewModels.TaskData");
        //    var tasks = viewModel.AutoProperty(typeof(List<>).GetDescriptor(validate, taskDataType), "Tasks");
        //    viewModel.Instance().PropertyGetter(tasks, o =>
        //    {
        //        if (o != null && o is IList list && list.Count == 0)
        //        {
        //            return true;
        //        }
        //        return false;
        //    }, o => "The 'Tasks' property should be initialized.");
        //    return viewModel;
        //}

        //[ValidationMethod]
        //public static CSharpClass Lesson2Step7Validator(CSharpValidate validate)
        //{
        //    var viewModel = Lesson2Step6Validator(validate);
        //    var addedTaskTitle = viewModel.Property<string>("AddedTaskTitle");
        //    var taskDataType = validate.Descriptor("DotvvmAcademy.Tutorial.ViewModels.TaskData");
        //    var tasks = viewModel.AutoProperty(typeof(List<>).GetDescriptor(validate, taskDataType), "Tasks");
        //    var instance = viewModel.Instance();

        //    instance.PropertySetter(addedTaskTitle, validate.TestingValue);
        //    instance.MethodCall(viewModel.Method("AddTask"));
        //    instance.PropertyGetter(addedTaskTitle, "", "The 'AddedTaskTitle' property should contain an empty string.");
        //    instance.PropertyGetter(tasks, o =>
        //    {
        //        if (o != null && o is IList list && list.Count == 1)
        //        {
        //            return ((dynamic)list[0]).Title == validate.TestingValue;
        //        }
        //        return false;
        //    }, o => "The 'Tasks' property doesn't contain a 'TaskData' object with its 'Title' property set to the correct value.");

        //    return viewModel;
        //}

        //[ValidationMethod]
        //public static DothtmlControl Lesson2Step8Validator(DothtmlValidate validate)
        //{
        //    Lesson2Step4Validator(validate);
        //    var repeater = validate.Root.Elements()[1].Control<Repeater>();
        //    repeater.Property(ItemsControl.DataSourceProperty).ValueBinding("Tasks");
        //    var itemTemplate = repeater.Property(Repeater.ItemTemplateProperty).TemplateContent();
        //    var div = itemTemplate.Element("div");
        //    div.Attribute("class").HardcodedValue("task");
        //    return div;
        //}

        //[ValidationMethod]
        //public static DothtmlControl Lesson2Step9Validator(DothtmlValidate validate)
        //{
        //    var div = Lesson2Step8Validator(validate);
        //    var literal = div.Control<Literal>();
        //    literal.Property(Literal.TextProperty).ValueBinding("Title");
        //    var link = div.Control<LinkButton>();
        //    return div;
        //}
    }
}