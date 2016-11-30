using DotvvmAcademy.Steps.Validation.Interfaces;
using DotvvmAcademy.Steps.Validation.ValidatorProvision;
using DotvvmAcademy.Steps.Validation.Validators.CommonValidators;
using DotvvmAcademy.Steps.Validation.Validators.PropertyAndControlType;
using DotVVM.Framework.Compilation.ControlTree.Resolved;
using DotVVM.Framework.Compilation.Parser.Dothtml.Parser;
using DotVVM.Framework.Controls;
using System.Linq;
using DotVVM.Framework.Utils;

namespace DotvvmAcademy.Steps.Validation.Validators.Lesson4
{
    [StepValidation(ValidatorKey = "Lesson4Step5Validator")]
    public class Lesson4Step5Validator : IDotHtmlCodeValidationObject
    {
        public void Validate(ResolvedTreeRoot resolvedTreeRoot)
        {
           
            var codeValidationException = new CodeValidationException("You shold enwrap div`s with one div");

            DotHtmlCommonValidator.CheckTypeAndCountHtmlTag(resolvedTreeRoot, "div",1, codeValidationException);

            var property = new Property("has-error", "none", ControlBindName.DivValidatorInvalidCssClass);
            DotHtmlCommonValidator.ValidatePropertyBinding(resolvedTreeRoot, property);

            property.TargetControlBindName = ControlBindName.DivValidatorInvalidCssClassRemove;
            var contentNode = resolvedTreeRoot.GetDescendantControls<HtmlGenericControl>().
               FirstOrDefault(d => d.DothtmlNode.As<DothtmlElementNode>()?.TagName == "div");

            DotHtmlCommonValidator.CheckTypeAndCountHtmlTag(contentNode, "div", 3);
            DotHtmlCommonValidator.ValidatePropertyBinding(contentNode, property);
        }
    }
}