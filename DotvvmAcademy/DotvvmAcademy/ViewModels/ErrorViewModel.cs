using DotVVM.Framework.ViewModel;
using System.Threading.Tasks;

namespace DotvvmAcademy.ViewModels
{
    public class ErrorViewModel : DotvvmAcademyViewModelBase
    {
        public string Message { get; set; }

        [FromRoute(nameof(StatusCode))]
        public int StatusCode { get; set; }

        public override Task Init()
        {
            base.Init().Wait();
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
            return Task.CompletedTask;
        }
    }
}