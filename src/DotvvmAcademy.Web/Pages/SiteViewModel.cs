using DotVVM.Framework.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DotvvmAcademy.Web.Pages
{
    public abstract class SiteViewModel : DotvvmViewModelBase
    {
        public const string DefaultLanguageMoniker = "en";
        public bool IsLandingPage { get; set; }
        [Bind(Direction.ServerToClientFirstRequest)]
        public LanguageOption Language { get; set; }

        [FromRoute("Language")]
        [Bind(Direction.ServerToClientFirstRequest)]
        public string LanguageMoniker { get; set; }

        [Bind(Direction.ServerToClientFirstRequest)]
        public List<LanguageOption> Languages { get; set; }

        public override Task Init()
        {
            if (string.IsNullOrEmpty(LanguageMoniker))
            {
                LanguageMoniker = DefaultLanguageMoniker;
            }
            Language = LanguageOption.Create(LanguageMoniker);
            return base.Init();
        }
    }
}