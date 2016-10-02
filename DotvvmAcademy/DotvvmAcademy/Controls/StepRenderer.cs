using DotVVM.Framework.Binding;
using DotVVM.Framework.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotVVM.Framework.Hosting;
using DotvvmAcademy.Steps;
using DotVVM.Framework.Compilation;

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
            if (DataContext.GetType() == typeof(Steps.InfoStep))
            {
                var builder = controlBuilderFactory.GetControlBuilder("Controls/InfoStep.dotcontrol");
                control = builder.BuildControl(controlBuilderFactory);
            }
            else if (DataContext.GetType() == typeof(Steps.ChoicesStep))
            {
                var builder = controlBuilderFactory.GetControlBuilder("Controls/ChoicesStep.dotcontrol");
                control = builder.BuildControl(controlBuilderFactory);
            }
            else if (DataContext.GetType() == typeof(Steps.DothtmlStep))
            {
                var builder = controlBuilderFactory.GetControlBuilder("Controls/DothtmlStep.dotcontrol");
                control = builder.BuildControl(controlBuilderFactory);
            }
            else if (DataContext.GetType() == typeof(Steps.CodeStep))
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
