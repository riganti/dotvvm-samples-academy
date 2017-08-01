using DotVVM.Framework.Controls;
using DotVVM.Framework.Hosting;
using DotvvmAcademy.Steps;

namespace DotvvmAcademy.Controls
{
    public class DothtmlStep : DotvvmMarkupControl
    {
        private CodeStepDotHtml ViewModel => (CodeStepDotHtml)DataContext;

        protected override void OnInit(IDotvvmRequestContext context)
        {
            if (!context.IsPostBack)
            {
                ViewModel.ResetCode();
            }
            base.OnInit(context);
        }
    }
}