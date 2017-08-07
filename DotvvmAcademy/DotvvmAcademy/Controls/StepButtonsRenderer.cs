using DotVVM.Framework.Compilation;
using DotVVM.Framework.Controls;
using DotVVM.Framework.Hosting;
using DotvvmAcademy.Steps.StepsBases;
using DotvvmAcademy.Steps.Validation.Interfaces;

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

            if (DataContext is CodeStepBase<IDotHtmlCodeValidationObject> ||
                DataContext is CodeStepBase<ICSharpCodeValidationObject>)
            {
                var builder = controlBuilderFactory.GetControlBuilder("Controls/CodeEditorButtons.dotcontrol");
                var control = builder.BuildControl(controlBuilderFactory);
                control.SetValue(Internal.UniqueIDProperty, "c1");
                Children.Add(control);
            }
            context.ChangeCurrentCulture(context.Parameters["Lang"].ToString());
            base.OnInit(context);
        }
    }
}