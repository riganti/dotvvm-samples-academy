using DotVVM.Framework.Binding;
using DotVVM.Framework.Controls;
using System.Collections.Generic;

namespace DotvvmAcademy.Web.Controls
{
    public class LanguageSwitch : DotvvmMarkupControl
    {
        public const string CurrentImageFormat = "images/Icons/Flag_{0}.svg";

        public static readonly DotvvmProperty CurrentLanguageProperty
            = DotvvmProperty.Register<string, LanguageSwitch>(c => c.CurrentLanguage, null);

        public static readonly DotvvmProperty AvailableLanguagesProperty
            = DotvvmProperty.Register<IEnumerable<string>, LanguageSwitch>(c => c.AvailableLanguages, null);

        public static readonly DotvvmProperty RouteNameProperty
            = DotvvmProperty.Register<string, LanguageSwitch>(c => c.RouteName, null);

        public IEnumerable<string> AvailableLanguages
        {
            get { return (IEnumerable<string>)GetValue(AvailableLanguagesProperty); }
            set { SetValue(AvailableLanguagesProperty, value); }
        }

        public string CurrentLanguage
        {
            get { return (string)GetValue(CurrentLanguageProperty); }
            set { SetValue(CurrentLanguageProperty, value); }
        }

        public string RouteName
        {
            get { return (string)GetValue(RouteNameProperty); }
            set { SetValue(RouteNameProperty, value); }
        }

        public string CurrentImage => string.Format(CurrentImageFormat, CurrentLanguage);
    }
}