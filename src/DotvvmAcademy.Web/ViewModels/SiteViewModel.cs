using DotVVM.Framework.ViewModel;

namespace DotvvmAcademy.Web.ViewModels
{
    public class SiteViewModel : DotvvmViewModelBase
    {
        [FromRoute("Language")]
        public string Language { get; set; }
    }
}