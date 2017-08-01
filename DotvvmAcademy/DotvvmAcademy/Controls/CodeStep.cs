using DotVVM.Framework.Controls;
using DotVVM.Framework.Hosting;
using DotvvmAcademy.Steps;

namespace DotvvmAcademy.Controls
{
    public class CodeStep : DotvvmMarkupControl
    {
        private CodeStepCsharp ViewModel => (CodeStepCsharp)DataContext;

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