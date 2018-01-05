using System;
using DotVVM.Framework.Compilation;
using DotVVM.Framework.Controls;
using DotVVM.Framework.Hosting;
using DotvvmAcademy.Steps;

namespace DotvvmAcademy.Controls
{
    public class StepRenderer : HtmlGenericControl
    {
        public StepRenderer() : base("div")
        {
        }

        protected override void OnInit(IDotvvmRequestContext context)
        {
            var controlBuilderFactory = context.Configuration.ServiceLocator.GetService<IControlBuilderFactory>();

            DotvvmControl control;
            if (DataContext.GetType() == typeof(InfoStep))
            {
                var builder = controlBuilderFactory.GetControlBuilder("Controls/InfoStep.dotcontrol");
                control = builder.BuildControl(controlBuilderFactory);
            }
            else if (DataContext.GetType() == typeof(ChoicesStep))
            {
                var builder = controlBuilderFactory.GetControlBuilder("Controls/ChoicesStep.dotcontrol");
                control = builder.BuildControl(controlBuilderFactory);
            }
            else if (DataContext.GetType() == typeof(CodeStepDotHtml))
            {
                var builder = controlBuilderFactory.GetControlBuilder("Controls/DothtmlStep.dotcontrol");
                control = builder.BuildControl(controlBuilderFactory);
            }
            else if (DataContext.GetType() == typeof(CodeStepCsharp))
            {
                var builder = controlBuilderFactory.GetControlBuilder("Controls/CodeStep.dotcontrol");
                control = builder.BuildControl(controlBuilderFactory);
            }
            else
            {
                throw new NotSupportedException();
            }
            Children.Add(control);

            base.OnInit(context);
        }
    }
}