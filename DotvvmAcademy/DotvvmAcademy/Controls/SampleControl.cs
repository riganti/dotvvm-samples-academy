using DotVVM.Framework.Binding;
using DotVVM.Framework.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotvvmAcademy.Controls
{
    public class SampleControl : DotvvmControl
    {
        public string IncorrectPath
        {
            get { return (string)GetValue(IncorrectPathProperty); }
            set { SetValue(IncorrectPathProperty, value); }
        }
        public static readonly DotvvmProperty IncorrectPathProperty
            = DotvvmProperty.Register<string, SampleControl>(c => c.IncorrectPath, null);

        public string CorrectPath
        {
            get { return (string)GetValue(CorrectPathProperty); }
            set { SetValue(CorrectPathProperty, value); }
        }
        public static readonly DotvvmProperty CorrectPathProperty
            = DotvvmProperty.Register<string, SampleControl>(c => c.CorrectPath, null);

        public string ValidatorName
        {
            get { return (string)GetValue(ValidatorNameProperty); }
            set { SetValue(ValidatorNameProperty, value); }
        }
        public static readonly DotvvmProperty ValidatorNameProperty
            = DotvvmProperty.Register<string, SampleControl>(c => c.ValidatorName, null);
    }
}
