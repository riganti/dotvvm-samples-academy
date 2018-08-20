using DotVVM.Framework.ViewModel;
using DotvvmAcademy.Web.Resources.Localization;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotvvmAcademy.Web.ViewModels
{
    public abstract class SiteViewModel : DotvvmViewModelBase
    {
        public LanguageOption Language { get; set; }

        [FromRoute("Language")]
        public string LanguageMoniker { get; set; }

        public List<LanguageOption> Languages { get; set; }

        public override Task Init()
        {
            if (string.IsNullOrEmpty(LanguageMoniker))
            {
                LanguageMoniker = "en";
            }
            return base.Init();
        }

        public override async Task Load()
        {
            Languages = (await GetAvailableLanguageMonikers())
                .Select(moniker =>
                {
                    var name = UIResources.ResourceManager.GetString($"Base_Language_{moniker}");
                    return new LanguageOption(moniker, name);
                })
                .ToList();
            Language = Languages.Single(l => l.Moniker == LanguageMoniker);
            await base.Load();
        }

        protected abstract Task<IEnumerable<string>> GetAvailableLanguageMonikers();
    }
}