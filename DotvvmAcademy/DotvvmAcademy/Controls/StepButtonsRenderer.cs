using DotvvmAcademy.Steps.StepsBases;
using DotvvmAcademy.Steps.Validation.Validators;
using DotVVM.Framework.Compilation;
using DotVVM.Framework.Controls;
using DotVVM.Framework.Hosting;

namespace DotvvmAcademy.Controls
{
    public class StepButtonsRenderer : HtmlGenericControl
    {
        public StepButtonsRenderer() : base("div")
        {
        }

        protected override void OnInit(IDotvvmRequestContext context)
        {
            var controlBuilderFactory = context.Configuration.ServiceLocator.GetService<IControlBuilderFactory>();

            DotvvmControl control;
            if (DataContext is CodeStepBase<IDotHtmlCodeStepValidationObject> ||
                DataContext is CodeStepBase<ICSharpCodeStepValidationObject>)
            {
                var builder = controlBuilderFactory.GetControlBuilder("Controls/CodeEditorButtons.dotcontrol");
                control = builder.BuildControl(controlBuilderFactory);
                Children.Add(control);
            }
            base.OnInit(context);
        }
    }
}