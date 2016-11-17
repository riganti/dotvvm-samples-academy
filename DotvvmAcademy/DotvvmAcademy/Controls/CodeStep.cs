using DotvvmAcademy.Steps;
using DotVVM.Framework.Controls;
using DotVVM.Framework.Hosting;

namespace DotvvmAcademy.Controls
{
    public class CodeStep : DotvvmMarkupControl
    {
        private CodeStepCsharp ViewModel => (CodeStepCsharp) DataContext;

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