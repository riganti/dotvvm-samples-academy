using DotvvmAcademy.BL.Validation.Dothtml;
using DotvvmAcademy.BL.Validation;
using DotVVM.Framework.Controls;
using System.Linq;
using DotvvmAcademy.BL.Validation.CSharp;
using System.Collections;
using System.Collections.Generic;

namespace DotvvmAcademy.BL.Validators
{
    public static class BasicMvvmValidators
    {
        [Validator(nameof(Lesson1Step3Validator))]
        public static (DothtmlControlCollection textBoxes, DothtmlControlCollection buttons) Lesson1Step3Validator(DothtmlValidate validate)
        {
            var paragraphs = validate.Root.Elements("p", 2).ToList();
            var first = paragraphs[0];
            var textBoxes = first.Controls<TextBox>(3);

            var second = paragraphs[1];
            var buttons = second.Controls<Button>(1);

            return (textBoxes, buttons);
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
        public static void Lesson1Step6Validator(DothtmlValidate validate)
        {
            var textBoxes = Lesson1Step3Validator(validate).textBoxes;
            textBoxes[0].AttributeValue("Text", "Hello");
            textBoxes[1].AttributeValue("Text", "Hello");
            textBoxes[2].AttributeValue("Text", "Hello");
        }

        [Validator(nameof(Lesson1Step7Validator))]
        public static void Lesson1Step7Validator(DothtmlValidate validate)
        {

        }
    }
}