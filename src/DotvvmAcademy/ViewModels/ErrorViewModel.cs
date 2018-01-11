using DotVVM.Framework.ViewModel;
using DotvvmAcademy.Resources;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;

namespace DotvvmAcademy.ViewModels
{
    public class ErrorViewModel : DotvvmAcademyViewModelBase
    {
        public ErrorViewModel(IStringLocalizer<UIResources> localizer) : base(localizer)
        {
        }

        public string Message { get; set; }

        [FromRoute(nameof(StatusCode))]
        public int StatusCode { get; set; }

        public override Task Init()
        {
            base.Init().Wait();
            switch (StatusCode)
            {
                case 404:
                    {
                        Message = UIResources.Error404;
                        break;
                    }
                case 500:
                    {
                        Message = UIResources.Error500;
                        break;
                    }
                default:
                    Message = UIResources.ErrorOther;
                    break;
            }
            return Task.CompletedTask;
        }
    }
}