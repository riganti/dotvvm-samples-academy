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
        public void SetCurrentLanguage()
        {
            Context.ChangeCurrentCulture(Context.Parameters["Lang"].ToString());
        }

        public string OnlineCourseText { get; set; }
        public string DocumentationText { get; set; }
        public string SamplesText { get; set; }
        public string TutorialsText { get; set; }

        public void SwitchLanguage(string language)
        {
            Context.RedirectToRoute(Context.Route.RouteName, new { Lang = language });
        }
    }
}