using System.Threading.Tasks;
using DotVVM.Framework.ViewModel;

namespace DotvvmAcademy.Web.ViewModels
{
    public class SiteViewModel : DotvvmViewModelBase
    {
        [FromRoute("Language")]
        public string Language { get; set; }

        public override Task Init()
        {
            if (string.IsNullOrEmpty(Language))
            {
                Language = "en";
            }
            return base.Init();
        }
    }
}