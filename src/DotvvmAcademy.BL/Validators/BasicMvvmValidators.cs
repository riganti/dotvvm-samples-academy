using DotVVM.Framework.Controls;
using DotvvmAcademy.Validation;
using DotvvmAcademy.Validation.CSharp.Unit;
using System.Linq;

namespace DotvvmAcademy.BL.Validators
{
    [ValidationClass]
    public static class BasicMvvmValidators
    {
        //[ValidationMethod]
        //public static (DothtmlControlCollection textBoxes, DothtmlControl button) Lesson1Step3Validator(DothtmlValidate validate)
        //{
        //    var paragraphs = validate.Root.Elements(2, "p");
        //    var first = paragraphs[0];
        //    var textBoxes = first.Controls<TextBox>(3);
        //    var second = paragraphs[1];
        //    var button = second.Control<Button>();

        //    return (textBoxes, button);
        //}

        [ValidationMethod]
        public static ICSharpClass Lesson1Step4Validator(ICSharpDocument document)
        {
            var tutorial = document.GetNamespace("DotvvmAcademy.Tutorial.ViewModels");
            var viewModel = tutorial.GetClass("Lesson1ViewModel");
            viewModel.GetAutoProperty<int>("Number1");
            viewModel.GetAutoProperty<int>("Number2");
            viewModel.GetAutoProperty<int>("Result");
            return viewModel;
        }

        [ValidationMethod]
        public static void Lesson1Step5Validator(ICSharpDocument document)
        {
            var viewModel = Lesson1Step4Validator(document);
            var calculate = viewModel.GetMethod("Calculate");

            //var instance = viewModel.Instance();
            //instance.PropertySetter(number1, 15);
            //instance.PropertySetter(number2, 30);
            //instance.MethodCall(calculate);
            //instance.PropertyGetter(result, 45);
        }

        //[ValidationMethod]
        //public static (DothtmlControlCollection textBoxes, DothtmlControl button) Lesson1Step6Validator(DothtmlValidate validate)
        //{
        //    (var textBoxes, var button) = Lesson1Step3Validator(validate);
        //    textBoxes[0].Property(TextBox.TextProperty).ValueBinding("Number1");
        //    textBoxes[1].Property(TextBox.TextProperty).ValueBinding("Number2");
        //    textBoxes[2].Property(TextBox.TextProperty).ValueBinding("Result");
        //    return (textBoxes, button);
        //}

        //[ValidationMethod]
        //public static (DothtmlControlCollection textBoxes, DothtmlControl button) Lesson1Step7Validator(DothtmlValidate validate)
        //{
        //    (var textBoxes, var button) = Lesson1Step6Validator(validate);
        //    button.Property(ButtonBase.TextProperty).HardcodedValue("Calculate!");
        //    button.Property(ButtonBase.ClickProperty).CommandBinding("Calculate()");
        //    return (textBoxes, button);
        //}
    }
}