using System.Linq;
using DotvvmAcademy.Lessons;
using DotvvmAcademy.Steps.Validation.Interfaces;
using DotVVM.Framework.Compilation.ControlTree.Resolved;
using DotVVM.Framework.Compilation.Parser.Dothtml.Parser;
using DotVVM.Framework.Controls;
using DotVVM.Framework.Utils;

namespace DotvvmAcademy.Steps.Validation.Validators.Lesson2
{
    [StepValidation(ValidationKey = "Lesson2Step13Validator")]
    public class Lesson2Step13Validator : IDotHtmlCodeValidationObject
    {
        public void Validate(ResolvedTreeRoot resolvedTreeRoot)
        {
            ValidatorHelper.ValidateRepeaterTemplate3(resolvedTreeRoot);

            var template = resolvedTreeRoot.GetDescendantControls<Repeater>().Single()
                .Properties[Repeater.ItemTemplateProperty]
                .CastTo<ResolvedPropertyTemplate>();

            var divs = template
                .GetDescendantControls<HtmlGenericControl>()
                .ToList();
            var div = divs.First(d => d.DothtmlNode.As<DothtmlElementNode>()?.TagName == "div");

            var classProperties = div.Properties
                .Where(p => p.Value.Property.Name == "Attributes:class")
                .Select(p => p.Value)
                .OfType<ResolvedPropertyBinding>()
                .ToList();
            if (classProperties.Count != 1)
            {
                throw new CodeValidationException(Lesson2Texts.InvalidClassBinding);
            }
            var classBinding = classProperties[0].Binding.Value.Replace(" ", "").Replace("\"", "'");
            if ((classBinding != "IsCompleted?'task-completed':'task'")
                && (classBinding != "IsCompleted==true?'task-completed':'task'")
                && (classBinding != "!IsCompleted?'task':'task-completed'")
                && (classBinding != "IsCompleted==false?'task':'task-completed'"))
            {
                throw new CodeValidationException(Lesson2Texts.InvalidClassBinding);
            }
        }
    }
}