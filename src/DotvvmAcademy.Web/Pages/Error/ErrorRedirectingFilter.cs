using DotVVM.Framework.Hosting;
using DotVVM.Framework.Runtime.Filters;
using DotvvmAcademy.Web.Resources.Localization;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DotvvmAcademy.Web.Pages.Error
{
    public class ErrorRedirectingFilter : ExceptionFilterAttribute
    {
        protected override Task OnCommandExceptionAsync(IDotvvmRequestContext context, ActionInfo actionInfo, Exception ex)
        {
            Redirect(context);
            return Task.CompletedTask;
        }

        protected override Task OnPageExceptionAsync(IDotvvmRequestContext context, Exception exception)
        {
            Redirect(context);
            return Task.CompletedTask;
        }

        protected override Task OnPresenterExceptionAsync(IDotvvmRequestContext context, Exception exception)
        {
            Redirect(context);
            return Task.CompletedTask;
        }

        private void Redirect(IDotvvmRequestContext context)
        {
            var language = UILanguage.DefaultLanguage;
            if (context.Parameters.ContainsKey("Language")
                && UILanguage.AvailableLanguages.Contains(context.Parameters["Language"]))
            {
                language = (string)context.Parameters["Language"];
            }
            context.RedirectToRoute("Error", new { ErrorCode = 500, Language = language }, true);
        }
    }
}