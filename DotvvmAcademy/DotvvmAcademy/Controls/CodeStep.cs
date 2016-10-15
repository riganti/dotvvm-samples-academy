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
                ViewModel.ResetCode();
            }
            base.OnInit(context);
        }

    }
}

