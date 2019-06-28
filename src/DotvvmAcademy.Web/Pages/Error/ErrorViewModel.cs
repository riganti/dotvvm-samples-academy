using DotVVM.Framework.ViewModel;
using DotvvmAcademy.Web.Resources.Localization;
using System.Linq;
using System.Threading.Tasks;

namespace DotvvmAcademy.Web.Pages.Error
{
    public class ErrorViewModel : SiteViewModel
    {
        [FromRoute("ErrorCode")]
        [Bind(Direction.ServerToClientFirstRequest)]
        public int ErrorCode { get; set; }

        [Bind(Direction.None)]
        public string ErrorMessage { get; set; }

        [Bind(Direction.None)]
        public string ErrorTitle { get; set; }

        public override Task Load()
        {
            Languages = DotvvmStartup.EnabledCultures.Select(LanguageOption.Create)
                .ToList();

            ErrorTitle = UIResources.ResourceManager.GetString($"Error_{ErrorCode}Title");
            ErrorMessage = UIResources.ResourceManager.GetString($"Error_{ErrorCode}Message");
            return base.Load();
        }
    }
}