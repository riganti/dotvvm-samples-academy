using DotVVM.Framework.ViewModel;

namespace DotvvmAcademy.Web.Pages.Error
{
    public class ErrorViewModel : SiteViewModel
    {
        [FromRoute("ErrorCode")]
        [Bind(Direction.ServerToClientFirstRequest)]
        public int ErrorCode { get; set; }
    }
}