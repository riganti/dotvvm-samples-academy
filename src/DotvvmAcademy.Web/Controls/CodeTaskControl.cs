using DotVVM.Framework.Binding;
using DotVVM.Framework.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotvvmAcademy.Web.Controls
{
    public class CodeTaskControl : HtmlGenericControl
    {
        public string Language
        {
            get { return (string)GetValue(LanguageProperty); }
            set { SetValue(LanguageProperty, value); }
        }

        public static readonly DotvvmProperty LanguageProperty
            = DotvvmProperty.Register<string, CodeTaskControl>(c => c.Language, null);

        public string Code
        {
            get { return (string)GetValue(CodeProperty); }
            set { SetValue(CodeProperty, value); }
        }

        public static readonly DotvvmProperty CodeProperty
            = DotvvmProperty.Register<string, CodeTaskControl>(c => c.Code, null);

        public CodeTaskControl() : base("div")
        {
        }


    }
}
