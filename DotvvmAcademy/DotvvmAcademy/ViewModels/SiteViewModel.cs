using System.Threading.Tasks;
using DotVVM.Framework.ViewModel;
using DotVVM.Framework.Hosting;
using System.Threading;
using System.Globalization;
using DotvvmAcademy.Lessons;

namespace DotvvmAcademy.ViewModels
{
    public class SiteViewModel : DotvvmViewModelBase
    {
        public void GetCurrentLanguage()
        {
            var culture = new CultureInfo(Context.Parameters["Lang"].ToString());
            CultureInfo.CurrentCulture = culture;
            CultureInfo.CurrentUICulture = culture;
        }

        public void SwitchLanguage(string language)
        {
            Context.RedirectToRoute(Context.Route.RouteName, new { Lang = language });
        }
    }
}