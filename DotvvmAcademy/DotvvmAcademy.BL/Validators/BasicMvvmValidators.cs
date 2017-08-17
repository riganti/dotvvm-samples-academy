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
        public static void Lesson1Step4Validator(CSharpValidate validate)
        {
        }

        [Validator(nameof(Lesson1Step5Validator))]
        public static void Lesson1Step5Validator(CSharpValidate validate)
        {
        }

        [Validator(nameof(Lesson1Step6Validator))]
        public static (DothtmlControlCollection textBoxes, DothtmlControl button) Lesson1Step6Validator(DothtmlValidate validate)
        {
            (var textBoxes, var button) = Lesson1Step3Validator(validate);
            textBoxes[0].ValueBinding(TextBox.TextProperty, "Number1");
            textBoxes[1].ValueBinding(TextBox.TextProperty, "Number2");
            textBoxes[2].ValueBinding(TextBox.TextProperty, "Result");
            return (textBoxes, button);
        }

        [Validator(nameof(Lesson1Step7Validator))]
        public static (DothtmlControlCollection textBoxes, DothtmlControl button) Lesson1Step7Validator(DothtmlValidate validate)
        {
            (var textBoxes, var button) = Lesson1Step6Validator(validate);
            button.HardcodedValue(ButtonBase.TextProperty, "Calculate!");
            button.CommandBinding(ButtonBase.ClickProperty, "Calculate()");
            return (textBoxes, button);
        }
    }
}