using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotVVM.Framework.Binding;
using DotVVM.Framework.Controls;

namespace DotvvmAcademy.Web.Controls.Icons
{
    public class Icon : DotvvmMarkupControl
    {
        public static readonly DotvvmProperty IconNameProperty = DotvvmProperty.Register<string, Icon>(c => c.IconName);

        [MarkupOptions(AllowBinding = true)]
        public string IconName
        {
            get => (string)GetValue(IconNameProperty);
            set => SetValue(IconNameProperty, value);
        }

    }
}

