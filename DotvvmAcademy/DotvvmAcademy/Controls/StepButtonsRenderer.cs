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
    public class StepButtonsRenderer : HtmlGenericControl
    {

        public StepButtonsRenderer() : base("div")
        {
        }

        protected override void OnInit(IDotvvmRequestContext context)
        {
            var controlBuilderFactory = context.Configuration.ServiceLocator.GetService<IControlBuilderFactory>();

            DotvvmControl control;
            if (DataContext is ICodeEditorStep)
            {
                var builder = controlBuilderFactory.GetControlBuilder("Controls/CodeEditorButtons.dotcontrol");
                control = builder.BuildControl(controlBuilderFactory);
                Children.Add(control);
            }

            base.OnInit(context);
        }

    }
}
