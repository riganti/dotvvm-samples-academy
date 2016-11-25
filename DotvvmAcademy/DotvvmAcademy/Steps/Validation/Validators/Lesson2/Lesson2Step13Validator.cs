using System.Collections.Generic;
using System.Linq;
using DotvvmAcademy.Steps.Validation.Interfaces;
using DotvvmAcademy.Steps.Validation.ValidatorProvision;
using DotVVM.Framework.Compilation.ControlTree.Resolved;
using DotVVM.Framework.Compilation.Parser.Dothtml.Parser;
using DotVVM.Framework.Controls;
using DotVVM.Framework.Utils;

namespace DotvvmAcademy.Steps.Validation.Validators.Lesson2
{
    [StepValidation(ValidatorKey = "Lesson2Step13Validator")]
    public class Lesson2Step13Validator : IDotHtmlCodeValidationObject
    {
        public void Validate(ResolvedTreeRoot resolvedTreeRoot)
        {
            Lesson2CommonValidator.ValidateRepeaterTemplate3(resolvedTreeRoot);

            var template = resolvedTreeRoot.GetDescendantControls<Repeater>().Single()
                .Properties[Repeater.ItemTemplateProperty]
                .CastTo<ResolvedPropertyTemplate>();


            //todo ControlBindName.DivClass
            // DotHtmlValidator.ValidatePropertyBinding

            List<ResolvedControl> resolvedControls = template
                .GetDescendantControls<HtmlGenericControl>()
                .ToList();

            ResolvedControl div = resolvedControls.First(d => d.DothtmlNode.As<DothtmlElementNode>()?.TagName == "div");


            //todo
            List<ResolvedPropertyBinding> divClassProperties = div.Properties
                .Where(p => p.Value.Property.Name == "Attributes:class")
                .Select(p => p.Value)
                .OfType<ResolvedPropertyBinding>()
                .ToList();


            if (divClassProperties.Count != 1)
            {
                throw new CodeValidationException(Lesson2Texts.InvalidClassBinding);
            }


            var classBinding = divClassProperties[0].Binding.Value.Replace(" ", "").Replace("\"", "'");
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