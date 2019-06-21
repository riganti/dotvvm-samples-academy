using DotVVM.Framework.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DotvvmAcademy.Web.Pages
{
    public abstract class SiteViewModel : DotvvmViewModelBase
    {
        public bool IsLandingPage { get; set; }

        [Bind(Direction.ServerToClientFirstRequest)]
        public LanguageOption Language { get; set; }

        [FromRoute("Language")]
        [Bind(Direction.ServerToClientFirstRequest)]
        public string LanguageMoniker { get; set; }

        [Bind(Direction.ServerToClientFirstRequest)]
        public List<LanguageOption> Languages { get; set; }

        public string CurrentRoute => Context.Route.RouteName;

        public override Task Init()
        {
            if (string.IsNullOrEmpty(LanguageMoniker))
            {
                LanguageMoniker = DotvvmStartup.DefaultCulture;
            }
            Language = LanguageOption.Create(LanguageMoniker);
            return base.Init();
        }
    }
}