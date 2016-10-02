using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotVVM.Framework.Controls;
using DotVVM.Framework.Hosting;

namespace DotvvmAcademy.Controls
{
	public class CodeStep : DotvvmMarkupControl
	{

        private Steps.CodeStep ViewModel => (Steps.CodeStep)DataContext;

        protected override void OnInit(IDotvvmRequestContext context)
        {
            if (!context.IsPostBack)
            {
                ResetCode();
            }
            base.OnInit(context);
        }

        public void ResetCode()
        {
            ViewModel.Code = ViewModel.StartupCode;
        }

        public void ShowCorrectCode()
        {
            ViewModel.Code = ViewModel.FinalCode;
        }

    }
}

