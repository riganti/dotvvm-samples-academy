using DotVVM.Framework.Controls;
using DotvvmAcademy.BL.Validation;
using DotvvmAcademy.BL.Validation.CSharp;
using DotvvmAcademy.BL.Validation.Dothtml;
using System.Linq;

namespace DotvvmAcademy.BL.Validators
{
    public static class BasicMvvmValidators
    {
        [Validator(nameof(Lesson1Step3Validator))]
        public static (DothtmlControlCollection textBoxes, DothtmlControl button) Lesson1Step3Validator(DothtmlValidate validate)
        {
            var paragraphs = validate.Root.Elements("p", 2).ToList();
            var first = paragraphs[0];
            var textBoxes = first.Controls<TextBox>(3);

            var second = paragraphs[1];
            var button = second.Control<Button>();

            return (textBoxes, button);
        }

        [Validator(nameof(Lesson1Step4Validator))]
        public static CSharpClass Lesson1Step4Validator(CSharpValidate validate)
        {
            validate.Root.Usings("System");
            var tutorial = validate.Root.Namespace("DotvvmAcademy.Tutorial.ViewModels");
            var viewModel = tutorial.Class("Lesson1ViewModel");
            viewModel.AutoProperty<int>("Number1");
            viewModel.AutoProperty<int>("Number2");
            viewModel.AutoProperty<int>("Result");
            return viewModel;
        }

        [Validator(nameof(Lesson1Step5Validator))]
        public static void Lesson1Step5Validator(CSharpValidate validate)
        {
            var viewModel = Lesson1Step4Validator(validate);
            var calculate = viewModel.Method("Calculate", typeof(void));
            var number1 = viewModel.Property<int>("Number1");
            var number2 = viewModel.Property<int>("Number2");
            var result = viewModel.Property<int>("Result");

            var instance = viewModel.Instance();
            instance.PropertySetter(number1, 15);
            instance.PropertySetter(number2, 30);
            instance.MethodExecution(calculate);
            instance.PropertyGetter(result, 45);
        }

        [Validator(nameof(Lesson1Step6Validator))]
        public static (DothtmlControlCollection textBoxes, DothtmlControl button) Lesson1Step6Validator(DothtmlValidate validate)
        {
            (var textBoxes, var button) = Lesson1Step3Validator(validate);
            textBoxes[0].Property(TextBox.TextProperty).ValueBinding("Number1");
            textBoxes[1].Property(TextBox.TextProperty).ValueBinding("Number2");
            textBoxes[2].Property(TextBox.TextProperty).ValueBinding("Result");
            return (textBoxes, button);
        }

        [Validator(nameof(Lesson1Step7Validator))]
        public static (DothtmlControlCollection textBoxes, DothtmlControl button) Lesson1Step7Validator(DothtmlValidate validate)
        {
            (var textBoxes, var button) = Lesson1Step6Validator(validate);
            button.Property(ButtonBase.TextProperty).HardcodedValue("Calculate!");
            button.Property(ButtonBase.ClickProperty).CommandBinding("Calculate()");
            return (textBoxes, button);
        }
    }
}