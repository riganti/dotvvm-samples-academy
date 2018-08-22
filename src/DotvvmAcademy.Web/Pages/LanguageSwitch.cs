using DotVVM.Framework.Binding;
using DotVVM.Framework.Controls;
using System.Collections.Generic;

namespace DotvvmAcademy.Web.Pages
{
    public class LanguageSwitch : DotvvmMarkupControl
    {
        public const string CurrentImageFormat = "images/Icons/Flag_{0}.svg";

        public static readonly DotvvmProperty AvailableLanguagesProperty
            = DotvvmProperty.Register<IEnumerable<LanguageOption>, LanguageSwitch>(c => c.AvailableLanguages, null);

        public static readonly DotvvmProperty CurrentLanguageProperty
            = DotvvmProperty.Register<LanguageOption, LanguageSwitch>(c => c.CurrentLanguage, null);

        public static readonly DotvvmProperty RouteNameProperty
            = DotvvmProperty.Register<string, LanguageSwitch>(c => c.RouteName, null);

        public IEnumerable<LanguageOption> AvailableLanguages
        {
            get { return (IEnumerable<LanguageOption>)GetValue(AvailableLanguagesProperty); }
            set { SetValue(AvailableLanguagesProperty, value); }
        }

        public string CurrentImage => string.Format(CurrentImageFormat, CurrentLanguage.Moniker);

        public LanguageOption CurrentLanguage
        {
            get { return (LanguageOption)GetValue(CurrentLanguageProperty); }
            set { SetValue(CurrentLanguageProperty, value); }
        }

        public string RouteName
        {
            get { return (string)GetValue(RouteNameProperty); }
            set { SetValue(RouteNameProperty, value); }
        }
    }
}