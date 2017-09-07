using System.Threading.Tasks;
using DotVVM.Framework.ViewModel;

namespace DotvvmAcademy.ViewModels
{
    public class ErrorViewModel : DotvvmAcademyViewModelBase
    {
        [FromRoute(nameof(StatusCode))]
        public int StatusCode { get; set; }

        public string Message { get; set; }

        public override Task Init()
        {
            switch (StatusCode)
            {
                case 500:
                    {
                        Message = "Internal server error";
                        break;
                    }
                case 404:
                    {
                        Message = "Page Not Found";
                        break;
                    }
                default:
                    Message = "Something bad happened and we have no idea what";
                    break;
            }
            return base.Init();
        }
    }
}