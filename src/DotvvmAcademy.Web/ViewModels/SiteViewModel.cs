using DotVVM.Framework.ViewModel;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotvvmAcademy.Web.ViewModels
{
    public abstract class SiteViewModel : DotvvmViewModelBase
    {
        [FromRoute("Language")]
        public string Language { get; set; }

        public List<string> Languages { get; set; }

        public override Task Init()
        {
            if (string.IsNullOrEmpty(Language))
            {
                Language = "en";
            }
            return base.Init();
        }

        public override async Task Load()
        {
            Languages = (await GetAvailableLanguages()).ToList();
            await base.Load();
        }

        protected abstract Task<IEnumerable<string>> GetAvailableLanguages();
    }
}